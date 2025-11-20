using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized; // 追加: リストの変更検知用
using System.IO;                      // 追加: ファイル保存用
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private LocationManagerWindow _managerWindow;
        private static readonly HttpClient client = new HttpClient();
        private const string SaveFileName = "saved_locations.json"; // 保存ファイル名

        public ObservableCollection<LocationData> WorldData { get; set; } = new ObservableCollection<LocationData>();
        public ObservableCollection<LocationData> JapanData { get; set; } = new ObservableCollection<LocationData>();
        public ObservableCollection<LocationData> SavedData { get; set; } = new ObservableCollection<LocationData>();

        // 現在地用プロパティ
        public string CurrentLocationName { get; set; } = "東京";
        public string CurrentCondition { get; set; } = "-";
        public string CurrentTemp { get; set; } = "-";

        public MainWindow() {
            InitializeComponent();
            this.DataContext = this;

            ListWorld.ItemsSource = WorldData;
            ListJapan.ItemsSource = JapanData;
            ListSaved.ItemsSource = SavedData;

            // ★重要: 登録リストに変更があった場合（追加時など）のイベントを登録
            SavedData.CollectionChanged += SavedData_CollectionChanged;

            InitializeData();
        }

        private async void InitializeData() {
            // 世界のデフォルト
            WorldData.Add(new LocationData { Name = "ニューヨーク", Latitude = 40.71, Longitude = -74.01 });
            WorldData.Add(new LocationData { Name = "ロンドン", Latitude = 51.51, Longitude = -0.13 });
            WorldData.Add(new LocationData { Name = "パリ", Latitude = 48.85, Longitude = 2.35 });
            WorldData.Add(new LocationData { Name = "シンガポール", Latitude = 1.35, Longitude = 103.82 });

            // 日本のデフォルト
            JapanData.Add(new LocationData { Name = "札幌", Latitude = 43.06, Longitude = 141.35 });
            JapanData.Add(new LocationData { Name = "東京", Latitude = 35.69, Longitude = 139.69 });
            JapanData.Add(new LocationData { Name = "大阪", Latitude = 34.69, Longitude = 135.50 });
            JapanData.Add(new LocationData { Name = "福岡", Latitude = 33.59, Longitude = 130.40 });

            // ★重要: 保存されたデータを読み込む
            LoadSavedLocations();

            await UpdateAllWeather();
        }

        // ★追加: リストに変更があった（都市が追加された）時に自動で天気を更新する処理
        private async void SavedData_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            // 新しい項目が追加された場合
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (LocationData newItem in e.NewItems) {
                    // 「天気がまだ不明(-)」の場合のみ取得しに行く
                    if (newItem.Temperature == "-") {
                        var w = await GetWeather(newItem.Latitude, newItem.Longitude);
                        if (w != null) {
                            newItem.Temperature = w.Temperature + "℃";
                            newItem.WindSpeed = w.Windspeed + " km/h";
                            newItem.WeatherCode = w.Weathercode.ToString();
                        }
                    }
                }
                // 画面更新
                ListSaved.Items.Refresh();
            }
        }

        // ★追加: ファイルから保存済みデータを読み込む処理
        private void LoadSavedLocations() {
            try {
                if (File.Exists(SaveFileName)) {
                    string json = File.ReadAllText(SaveFileName);
                    var loadedList = JsonSerializer.Deserialize<List<LocationData>>(json);

                    if (loadedList != null) {
                        foreach (var item in loadedList) {
                            SavedData.Add(item);
                        }
                    }
                } else {
                    // ファイルがない場合（初回起動時）はデフォルト値を追加
                    SavedData.Add(new LocationData { Name = "那覇", Latitude = 26.21, Longitude = 127.68 });
                }
            }
            catch {
                // 読み込みエラー時はデフォルトを追加
                SavedData.Add(new LocationData { Name = "那覇", Latitude = 26.21, Longitude = 127.68 });
            }
        }

        // ★追加: データをファイルに保存する処理
        private void SaveSavedLocations() {
            try {
                // 見やすく整形して保存 (WriteIndented = true)
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(SavedData, options);
                File.WriteAllText(SaveFileName, json);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("保存エラー: " + ex.Message);
            }
        }

        private async Task UpdateAllWeather() {
            await UpdateList(WorldData);
            await UpdateList(JapanData);
            await UpdateList(SavedData);
            await UpdateCurrentLocation();
        }

        private async Task UpdateList(IEnumerable<LocationData> list) {
            foreach (var item in list) {
                var w = await GetWeather(item.Latitude, item.Longitude);
                if (w != null) {
                    item.Temperature = w.Temperature + "℃";
                    item.WindSpeed = w.Windspeed + " km/h";
                    item.WeatherCode = w.Weathercode.ToString();
                }
            }
            ListWorld.Items.Refresh();
            ListJapan.Items.Refresh();
            ListSaved.Items.Refresh();
        }

        private async Task UpdateCurrentLocation() {
            try {
                CurrentLocationName = "位置情報取得中...";
                this.DataContext = null;
                this.DataContext = this;

                // IPアドレスから位置情報を取得
                string geoUrl = "http://ip-api.com/json/?fields=status,city,lat,lon";
                string geoJson = await client.GetStringAsync(geoUrl);
                var geoData = JsonSerializer.Deserialize<IpGeoResponse>(geoJson);

                if (geoData != null && geoData.Status == "success") {
                    CurrentLocationName = $"{geoData.City} (現在地)";
                    var w = await GetWeather(geoData.Lat, geoData.Lon);
                    if (w != null) {
                        CurrentTemp = w.Temperature + "℃";
                        CurrentCondition = new LocationData { WeatherCode = w.Weathercode.ToString() }.DisplayWeather;
                    }
                } else {
                    CurrentLocationName = "位置特定不可";
                    CurrentCondition = "-";
                    CurrentTemp = "-";
                }
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                CurrentLocationName = "接続エラー";
            }

            this.DataContext = null;
            this.DataContext = this;
        }

        private async Task<CurrentWeather> GetWeather(double lat, double lon) {
            try {
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
                var json = await client.GetStringAsync(url);
                var res = JsonSerializer.Deserialize<OpenMeteoResponse>(json);
                return res?.CurrentWeather;
            }
            catch { return null; }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.Source is TabControl) {
                if (TabSaved.IsSelected) {
                    ShowManagerWindow();
                } else {
                    _managerWindow?.Hide();
                }

                if (MainTabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "現在地") {
                    if (CurrentLocationContentGrid != null) {
                        var sb = (Storyboard)FindResource("FadeInAnimation");
                        Storyboard.SetTarget(sb, CurrentLocationContentGrid);
                        sb.Begin();
                    }
                }
            }
        }

        private void ShowManagerWindow() {
            if (_managerWindow == null) {
                _managerWindow = new LocationManagerWindow(SavedData);
                _managerWindow.Owner = this;
            }

            _managerWindow.Left = this.Left + this.Width + 10;
            _managerWindow.Top = this.Top;
            _managerWindow.Show();
        }

        private void Window_LocationChanged(object sender, EventArgs e) {
            if (_managerWindow != null && _managerWindow.IsVisible) {
                _managerWindow.Left = this.Left + this.Width + 10;
                _managerWindow.Top = this.Top;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            Window_LocationChanged(sender, null);
        }

        // ★修正: アプリ終了時にデータを保存する
        private void Window_Closed(object sender, EventArgs e) {
            SaveSavedLocations(); // 保存処理を実行
            _managerWindow?.Close();
        }

        private async void RefreshCurrent_Click(object sender, RoutedEventArgs e) {
            await UpdateCurrentLocation();
        }
    }
}