using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace TenkiApp {
    public partial class LocationManagerWindow : Window {
        private ObservableCollection<LocationData> _targetList;
        private static readonly HttpClient client = new HttpClient();

        public LocationManagerWindow(ObservableCollection<LocationData> list) {
            InitializeComponent();
            _targetList = list;
            LocationListView.ItemsSource = _targetList;
            UpdatePlaceholderVisibility();
        }

        // ウィンドウを閉じる代わりに隠す
        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }

        // --- プレースホルダー制御 ---
        private void UpdatePlaceholderVisibility() {
            if (string.IsNullOrEmpty(SearchBox.Text))
                SearchPlaceholder.Visibility = Visibility.Visible;
            else
                SearchPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e) {
            SearchPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e) {
            UpdatePlaceholderVisibility();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            UpdatePlaceholderVisibility();
        }
        // ----------------------------------------------

        private async void AddLocation_Click(object sender, RoutedEventArgs e) {
            string query = SearchBox.Text;
            if (string.IsNullOrWhiteSpace(query)) return;

            try {
                // Open-MeteoのジオコーディングAPIは日本語検索(language=ja)に対応しています
                string url = $"https://geocoding-api.open-meteo.com/v1/search?name={query}&count=1&language=ja&format=json";
                string json = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<GeoSearchResponse>(json);

                if (data?.Results != null && data.Results.Count > 0) {
                    var res = data.Results[0];
                    _targetList.Add(new LocationData {
                        Name = res.Name,
                        Latitude = res.Latitude,
                        Longitude = res.Longitude
                    });
                    SearchBox.Text = "";
                } else {
                    MessageBox.Show("都市が見つかりませんでした。");
                }
            }
            catch { MessageBox.Show("検索エラーが発生しました。"); }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e) {
            int idx = LocationListView.SelectedIndex;
            if (idx > 0) _targetList.Move(idx, idx - 1);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e) {
            int idx = LocationListView.SelectedIndex;
            if (idx < _targetList.Count - 1 && idx >= 0) _targetList.Move(idx, idx + 1);
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            if (LocationListView.SelectedItem is LocationData item) {
                _targetList.Remove(item);
            }
        }
    }
}