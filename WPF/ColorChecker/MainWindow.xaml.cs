using System;
using System.Collections.Generic;
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

        private bool isUpdatingFromComboBox = false;

        public MainWindow() {
            InitializeComponent();
            InitializeColorSamples();
        }

        private void InitializeColorSamples() {
            colorListComboBox.ItemsSource = colorSamples;
            // The line below was removed to fix the error.
            // colorListComboBox.DisplayMemberPath = "Key"; 
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Application.Current.Shutdown();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (isUpdatingFromComboBox) {
                return;
            }

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
                if (item.Value.R == targetColor.R &&
                    item.Value.G == targetColor.G &&
                    item.Value.B == targetColor.B) {
                    return item;
                }
            }
            return null;
        }

        private void colorListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListComboBox.SelectedItem is KeyValuePair<string, Color> selectedItem) {
                Color selectedColor = selectedItem.Value;

                isUpdatingFromComboBox = true;

                redSlider.Value = selectedColor.R;
                greenSlider.Value = selectedColor.G;
                blueSlider.Value = selectedColor.B;

                isUpdatingFromComboBox = false;

                colorPreviewBorder.Background = new SolidColorBrush(selectedColor);
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

            string displayText;
            if (colorName != null) {
                displayText = colorName;
            } else {
                displayText = $"R:{r} G:{g} B:{b}";
            }

            colorListBox.Items.Add(new ColorEntry {
                Color = newColor,
                RgbText = displayText
            });

            colorListBox.ScrollIntoView(colorListBox.Items[colorListBox.Items.Count - 1]);
        }
    }

    public class ColorEntry {
        public Color Color { get; set; }
        public string RgbText { get; set; }
    }
}