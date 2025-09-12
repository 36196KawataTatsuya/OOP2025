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
        //デフォルトカラーを持ったディクショナリ
        private readonly Dictionary<string, Color> colorSamples = new Dictionary<string, Color> {
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
            { "サーモン", Colors.Salmon },
        };

        //色変更時にプレビューなどを変更するために使用する判定
        private bool isUpdatingUI = false;

        //データコレクションらしい
        public ObservableCollection<ColorEntry> StockedColors { get; set; }

        public MainWindow() {
            InitializeComponent();
            InitializeColorSamples();

            StockedColors = new ObservableCollection<ColorEntry>();
            colorListBox.ItemsSource = StockedColors;
        }

        private void InitializeColorSamples() {
            //一番上で作ったディクショナリにアクセスできるように
            colorListComboBox.ItemsSource = colorSamples;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            //何にもフォーカスせずESCを押してアプリを終了可能に
            if (e.Key == Key.Escape) {
                Application.Current.Shutdown();
            }
        }

        //RGBスライダーを移動させたときに実行
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (isUpdatingUI) return;

            //RGBの数値を取得
            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;

            //上記数値から色のプレビューを反映
            colorPreviewBorder.Background = new SolidColorBrush(Color.FromRgb(r, g, b));

            //色履歴リスト用コンボボックスにセットする機構
            Color currentColor = Color.FromRgb(r, g, b);
            var matchingItem = FindMatchingColor(currentColor);

            if (matchingItem != null) {
                colorListComboBox.SelectedItem = matchingItem;
            } else {
                colorListComboBox.SelectedItem = null;
                colorListComboBox.Text = string.Empty;
            }
        }

        //RGBがマッチした色があった時にコンボボックスに逆設定
        private object FindMatchingColor(Color targetColor) {
            foreach (KeyValuePair<string, Color> item in colorListComboBox.Items) {
                if (item.Value.R == targetColor.R && item.Value.G == targetColor.G && item.Value.B == targetColor.B) {
                    return item;
                }
            }
            return null;
        }

        //コンボボックスから色を選択した時の動作
        private void colorListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListComboBox.SelectedItem is KeyValuePair<string, Color> selectedItem) {
                Color selectedColor = selectedItem.Value;

                //スライダーに反映
                isUpdatingUI = true;
                redSlider.Value = selectedColor.R;
                greenSlider.Value = selectedColor.G;
                blueSlider.Value = selectedColor.B;

                //色のプレビューに反映
                colorPreviewBorder.Background = new SolidColorBrush(selectedColor);
                isUpdatingUI = false;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true;
            }
        }

        //現在のRGB色情報を保存
        private void saveColorButton_Click(object sender, RoutedEventArgs e) {
            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;
            Color newColor = Color.FromRgb(r, g, b);

            string colorName = colorSamples.FirstOrDefault(x => x.Value == newColor).Key;
            string displayText = colorName ?? $"R:{r} G:{g} B:{b}";

            //登録済みカラーの再登録を防ぎ、エラーメッセージを表示する
            if (StockedColors.Any(entry => entry.Color.R == newColor.R && entry.Color.G == newColor.G && entry.Color.B == newColor.B)) {
                MessageBox.Show("既に登録済みの色の再登録はできません\n別の色を登録するか、登録済みの色を削除してください", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StockedColors.Add(new ColorEntry { Color = newColor, RgbText = displayText });

            if (StockedColors.Any()) {
                colorListBox.ScrollIntoView(StockedColors.Last());
            }
        }

        //リストボックスから色を選択した場合の挙動
        private void colorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                Color selectedColor = selectedEntry.Color;
                isUpdatingUI = true;

                //選択した色のRGBをスライダーに反映
                redSlider.Value = selectedColor.R;
                greenSlider.Value = selectedColor.G;
                blueSlider.Value = selectedColor.B;

                //色のプレビューに反映
                colorPreviewBorder.Background = new SolidColorBrush(selectedColor);

                //もしコンボボックスに登録されている色とマッチする場合
                var matchingItem = FindMatchingColor(selectedColor);
                if (matchingItem != null) {
                    //コンボボックスも選択した状態にする
                    colorListComboBox.SelectedItem = matchingItem;
                } else {
                    colorListComboBox.SelectedItem = null;
                    colorListComboBox.Text = string.Empty;
                }
                isUpdatingUI = false;
            }
        }

        // ここからが新しいボタン(上下削除)用のメソッド

        //アイテムを上に移動する
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

        //アイテムを下に移動する
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

        //アイテムを削除する
        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (colorListBox.SelectedItem is ColorEntry selectedEntry) {
                StockedColors.Remove(selectedEntry);
            }
        }
    }

    //色の基本情報    
    public class ColorEntry {
        public Color Color { get; set; }
        public string RgbText { get; set; }
    }
}