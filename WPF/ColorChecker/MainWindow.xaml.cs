using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorChecker {
    public partial class MainWindow : Window {
        private readonly Dictionary<string, Color> colorSamples = new Dictionary<string, Color>
        {
            { "赤", Colors.Red },
            { "緑", Colors.Green },
            { "青", Colors.Blue },
            { "黄", Colors.Yellow },
            { "シアン", Colors.Cyan },
            { "マゼンタ", Colors.Magenta },
            { "白", Colors.White },
            { "灰色", Colors.Gray },
            { "黒", Colors.Black },
            { "オレンジ", Colors.Orange },
            { "紫", Colors.Purple },
            { "茶色", Colors.Brown },
            { "ピンク", Colors.Pink },
            { "黄緑", Colors.LimeGreen },
            { "水色", Colors.SkyBlue },
            { "紺", Colors.Navy },
            { "金色", Colors.Gold },
            { "銀色", Colors.Silver },
            { "ベージュ", Colors.Beige },
            { "深紅", Colors.Crimson },
            { "オリーブ", Colors.Olive },
            { "ターコイズ", Colors.Turquoise },
            { "藍色", Colors.Indigo },
            { "すみれ色", Colors.Violet },
            { "珊瑚色", Colors.Coral },
            { "サーモン", Colors.Salmon }
        };

        private bool isUpdatingUI = false;

        public ObservableCollection<ColorEntry> StockedColors { get; set; }

        public MainWindow() {
            InitializeComponent();
            InitializeColorSamples();

            StockedColors = new ObservableCollection<ColorEntry>();
            colorListBox.ItemsSource = StockedColors;
        }

        private void InitializeColorSamples() {
            colorListComboBox.ItemsSource = colorSamples;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Application.Current.Shutdown();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (isUpdatingUI) return;

            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;

            colorPreviewBorder.Background = new SolidColorBrush(Color.FromRgb(r, g, b));

            Color currentColor = Color.FromRgb(r, g, b);
            var matchingItem = FindMatchingColor(currentColor);

            if (matchingItem != null) {
                colorListComboBox.SelectedItem = matchingItem;
            } else {
                colorListComboBox.SelectedItem = null;
                colorListComboBox.Text = string.Empty;
            }
        }

        private object FindMatchingColor(Color targetColor) {
            foreach (KeyValuePair<string, Color> item in colorListComboBox.Items) {
                if (item.Value.R == targetColor.R && item.Value.G == targetColor.G && item.Value.B == targetColor.B) {
                    return item;
                }
            }
            return null;
        }

        private void colorListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListComboBox.SelectedItem is KeyValuePair<string, Color> selectedItem) {
                Color selectedColor = selectedItem.Value;

                isUpdatingUI = true;
                redSlider.Value = selectedColor.R;
                greenSlider.Value = selectedColor.G;
                blueSlider.Value = selectedColor.B;
                colorPreviewBorder.Background = new SolidColorBrush(selectedColor);
                isUpdatingUI = false;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true;
            }
        }

        private void saveColorButton_Click(object sender, RoutedEventArgs e) {
            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;
            Color newColor = Color.FromRgb(r, g, b);

            string colorName = colorSamples.FirstOrDefault(x => x.Value == newColor).Key;
            string displayText = colorName ?? $"R:{r} G:{g} B:{b}";

            StockedColors.Add(new ColorEntry { Color = newColor, RgbText = displayText });

            if (StockedColors.Any()) {
                colorListBox.ScrollIntoView(StockedColors.Last());
            }
        }

        private void colorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                Color selectedColor = selectedEntry.Color;
                isUpdatingUI = true;

                redSlider.Value = selectedColor.R;
                greenSlider.Value = selectedColor.G;
                blueSlider.Value = selectedColor.B;
                colorPreviewBorder.Background = new SolidColorBrush(selectedColor);

                var matchingItem = FindMatchingColor(selectedColor);
                if (matchingItem != null) {
                    colorListComboBox.SelectedItem = matchingItem;
                } else {
                    colorListComboBox.SelectedItem = null;
                    colorListComboBox.Text = string.Empty;
                }
                isUpdatingUI = false;
            }
        }

        // ▼▼▼ 【ここからが新しいボタン用のメソッド】 ▼▼▼

        private void MoveUpButton_Click(object sender, RoutedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                int currentIndex = StockedColors.IndexOf(selectedEntry);
                if (currentIndex > 0) {
                    StockedColors.Move(currentIndex, currentIndex - 1);
                    // 移動後も選択状態を維持
                    colorListBox.SelectedItem = selectedEntry;
                }
            }
        }

        private void MoveDownButton_Click(object sender, RoutedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                int currentIndex = StockedColors.IndexOf(selectedEntry);
                if (currentIndex >= 0 && currentIndex < StockedColors.Count - 1) {
                    StockedColors.Move(currentIndex, currentIndex + 1);
                    // 移動後も選択状態を維持
                    colorListBox.SelectedItem = selectedEntry;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                StockedColors.Remove(selectedEntry);
            }
        }
    }

    public class ColorEntry {
        public Color Color { get; set; }
        public string RgbText { get; set; }
    }
}