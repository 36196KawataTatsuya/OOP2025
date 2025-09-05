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
            { "青", Colors.Blue },
            { "緑", Colors.Green },
            { "黄", Colors.Yellow },
            { "シアン", Colors.Cyan },
            { "マゼンタ", Colors.Magenta },
            { "白", Colors.White },
            { "黒", Colors.Black },
            { "灰色", Colors.Gray },
            { "オレンジ", Colors.Orange }
        };

        private bool isUpdatingFromComboBox = false;

        public MainWindow() {
            InitializeComponent();
            InitializeColorSamples();
        }

        private void InitializeColorSamples() {
            foreach (var sample in colorSamples) {
                var comboBoxItem = new ComboBoxItem {
                    Content = sample.Key,
                    Tag = sample.Value
                };
                colorListComboBox.Items.Add(comboBoxItem);
            }
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

        private ComboBoxItem FindMatchingColor(Color targetColor) {
            foreach (ComboBoxItem item in colorListComboBox.Items) {
                if (item.Tag is Color color &&
                    color.R == targetColor.R &&
                    color.G == targetColor.G &&
                    color.B == targetColor.B) {
                    return item;
                }
            }
            return null;
        }

        private void colorListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListComboBox.SelectedItem is ComboBoxItem selectedItem) {
                if (selectedItem.Tag is Color selectedColor) {
                    isUpdatingFromComboBox = true;

                    redSlider.Value = selectedColor.R;
                    greenSlider.Value = selectedColor.G;
                    blueSlider.Value = selectedColor.B;

                    isUpdatingFromComboBox = false;

                    colorPreviewBorder.Background = new SolidColorBrush(selectedColor);
                }
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

            colorListBox.Items.Add(new ColorEntry {
                Color = Color.FromRgb(r, g, b),
                RgbText = $"R:{r} G:{g} B:{b}"
            });

            colorListBox.ScrollIntoView(colorListBox.Items[colorListBox.Items.Count - 1]);
        }
    }

    public class ColorEntry {
        public Color Color { get; set; }
        public string RgbText { get; set; }
    }
}