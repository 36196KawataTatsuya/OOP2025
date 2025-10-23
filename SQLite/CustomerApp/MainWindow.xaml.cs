using Microsoft.Win32;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics; // APIエラー確認用
using System.IO;
using System.Linq;
using System.Net.Http; // API通信用
using System.Net.Http.Headers; // API通信用
using System.Text.Json; // JSON解析用
using System.Text.Json.Serialization; // JSON解析用
using System.Threading.Tasks; // 非同期処理用
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // KeyDownイベント用
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CustomerApp {
    public partial class MainWindow : Window {
        private readonly string _dbPath;
        private SQLiteConnection _db;

        private ObservableCollection<Customer> _customers;

        private bool _deleteConfirmationRequired = true;

        private Customer? _selectedCustomer = null;

        private DispatcherTimer _popupTimer;

        // API通信用のクライアント (静的が推奨)
        private static readonly HttpClient _httpClient = new HttpClient();

        // 検索ソート順 (true: 降順, false: 昇順)
        private bool _isSortDescending = true;

        public MainWindow() {
            InitializeComponent();

            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomerDatabase.db");

            InitializeDatabase();

            _customers = new ObservableCollection<Customer>();
            customerListView.ItemsSource = _customers;

            InitializePopupTimer();

            // APIクライアントの初期設定
            _httpClient.BaseAddress = new Uri("https://jp-postal-code-api.ttskch.com/api/v1/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LoadCustomers();
            UpdateButtonStates();
        }

        private void InitializePopupTimer() {
            _popupTimer = new DispatcherTimer();
            _popupTimer.Tick += (s, e) => {
                notificationPopup.IsOpen = false;
                _popupTimer.Stop();
            };
            _popupTimer.Interval = TimeSpan.FromSeconds(2.5);
        }

        private void ShowNotification(string message) {
            popupText.Text = message;
            notificationPopup.IsOpen = true;

            _popupTimer.Stop();
            _popupTimer.Start();
        }

        private void InitializeDatabase() {
            try {
                _db = new SQLiteConnection(_dbPath);
                _db.CreateTable<Customer>();
            }
            catch (Exception ex) {
                MessageBox.Show($"データベースの初期化に失敗しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 顧客リストを読み込みます。検索語が指定されている場合はフィルタリングとソートを行います。
        /// </summary>
        /// <param name="searchTerm">検索文字列 (オプション)</param>
        private void LoadCustomers(string? searchTerm = null) {
            try {
                var allCustomers = _db.Table<Customer>().ToList();
                bool needsUpdate = false;

                // --- DisplayOrderが0のデータを処理 (既存ロジック) ---
                var customersWithZeroOrder = allCustomers.Where(c => c.DisplayOrder == 0).ToList();
                if (customersWithZeroOrder.Any()) {
                    needsUpdate = true;
                    int maxOrder = allCustomers.Where(c => c.DisplayOrder > 0)
                                               .Select(c => c.DisplayOrder)
                                               .DefaultIfEmpty(0).Max();

                    foreach (var customer in customersWithZeroOrder.OrderBy(c => c.Id)) {
                        maxOrder++;
                        customer.DisplayOrder = maxOrder;
                        _db.Update(customer);
                    }
                }

                if (needsUpdate) {
                    allCustomers = _db.Table<Customer>().ToList(); // 再読み込み
                }

                // --- 検索とソート処理 ---
                IEnumerable<Customer> finalCustomers;

                if (string.IsNullOrWhiteSpace(searchTerm)) {
                    // 検索なし: DisplayOrderでソート
                    finalCustomers = allCustomers.OrderBy(c => c.DisplayOrder);
                } else {
                    // 検索あり: フィルタリング
                    finalCustomers = allCustomers.Where(c =>
                        (c.Name != null && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (c.Phone != null && c.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (c.Address != null && c.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    );

                    // 検索結果のソート (氏名 -> 電話番号 -> 住所)
                    if (_isSortDescending) {
                        finalCustomers = finalCustomers
                            .OrderByDescending(c => c.Name)
                            .ThenByDescending(c => c.Phone)
                            .ThenByDescending(c => c.Address);
                    } else {
                        finalCustomers = finalCustomers
                            .OrderBy(c => c.Name)
                            .ThenBy(c => c.Phone)
                            .ThenBy(c => c.Address);
                    }
                }

                // リストを更新
                _customers.Clear();
                foreach (var customer in finalCustomers.ToList()) {
                    _customers.Add(customer);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"データ読み込みエラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // 検索状態に応じてボタンの状態を更新
            UpdateButtonStates();
        }


        private void btnRegister_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text)) {
                ShowNotification("氏名と電話番号は必須入力です");
                return;
            }

            try {
                int maxOrder = _customers.Any() ? _customers.Max(c => c.DisplayOrder) : 0;

                var newCustomer = new Customer {
                    Name = txtName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Picture = GetImageDataFromPreview(),
                    DisplayOrder = maxOrder + 1
                };

                _db.Insert(newCustomer);

                LoadCustomers();
                ClearInputFields();
            }
            catch (Exception ex) {
                MessageBox.Show($"登録エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) {
                ShowNotification("削除する顧客を選択してください");
                return;
            }

            if (_deleteConfirmationRequired) {
                var result = MessageBox.Show(
                    $"「{selectedCustomer.Name}」のデータを本当に削除しますか？\n(アプリケーションを終了するまで、この確認は再表示されません)",
                    "削除確認", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No) {
                    return;
                }
                _deleteConfirmationRequired = false;
            }

            try {
                _db.Delete(selectedCustomer);
                LoadCustomers(txtSearch.Text); // 検索状態を維持してリロード
                ClearInputFields();
            }
            catch (Exception ex) {
                MessageBox.Show($"削除エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) {
                ShowNotification("編集する顧客を選択してください");
                return;
            }

            _selectedCustomer = selectedCustomer;

            txtName.Text = _selectedCustomer.Name;
            txtPhone.Text = _selectedCustomer.Phone;
            txtAddress.Text = _selectedCustomer.Address;
            txtZipCode.Text = ""; // 郵便番号は保持しない (必要ならCustomerに追加)
            imgPreview.Source = LoadImageFromByteArray(_selectedCustomer.Picture);

            ToggleEditMode(true);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            if (_selectedCustomer == null) return;

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text)) {
                ShowNotification("氏名と電話番号は必須入力です");
                return;
            }

            try {
                _selectedCustomer.Name = txtName.Text.Trim();
                _selectedCustomer.Phone = txtPhone.Text.Trim();
                _selectedCustomer.Address = txtAddress.Text.Trim();
                _selectedCustomer.Picture = GetImageDataFromPreview();

                _db.Update(_selectedCustomer);

                LoadCustomers(txtSearch.Text); // 検索状態を維持してリロード
                ClearInputFields();
                ToggleEditMode(false);
                _selectedCustomer = null;
            }
            catch (Exception ex) {
                MessageBox.Show($"更新エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Button Click Handlers & Event Handlers

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            ClearInputFields();
            ToggleEditMode(false);
            _selectedCustomer = null;
            customerListView.SelectedItem = null;
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) return;

            int currentIndex = _customers.IndexOf(selectedCustomer);
            if (currentIndex > 0) {
                Customer upperCustomer = _customers[currentIndex - 1];

                (selectedCustomer.DisplayOrder, upperCustomer.DisplayOrder) =
                    (upperCustomer.DisplayOrder, selectedCustomer.DisplayOrder);

                try {
                    _db.Update(selectedCustomer);
                    _db.Update(upperCustomer);

                    int selectedId = selectedCustomer.Id;
                    LoadCustomers();

                    var customerToReselect = _customers.FirstOrDefault(c => c.Id == selectedId);
                    if (customerToReselect != null) {
                        customerListView.SelectedItem = customerToReselect;
                        customerListView.ScrollIntoView(customerToReselect);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"順序変更エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    (selectedCustomer.DisplayOrder, upperCustomer.DisplayOrder) =
                        (upperCustomer.DisplayOrder, selectedCustomer.DisplayOrder);
                }
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) return;

            int currentIndex = _customers.IndexOf(selectedCustomer);
            if (currentIndex < _customers.Count - 1) {
                Customer lowerCustomer = _customers[currentIndex + 1];

                (selectedCustomer.DisplayOrder, lowerCustomer.DisplayOrder) =
                    (lowerCustomer.DisplayOrder, selectedCustomer.DisplayOrder);

                try {
                    _db.Update(selectedCustomer);
                    _db.Update(lowerCustomer);

                    int selectedId = selectedCustomer.Id;
                    LoadCustomers();

                    var customerToReselect = _customers.FirstOrDefault(c => c.Id == selectedId);
                    if (customerToReselect != null) {
                        customerListView.SelectedItem = customerToReselect;
                        customerListView.ScrollIntoView(customerToReselect);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"順序変更エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    (selectedCustomer.DisplayOrder, lowerCustomer.DisplayOrder) =
                        (lowerCustomer.DisplayOrder, selectedCustomer.DisplayOrder);
                }
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Filter = "画像ファイル (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|すべてのファイル (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true) {
                try {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgPreview.Source = bitmap;
                }
                catch (Exception ex) {
                    MessageBox.Show($"画像読み込みエラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    imgPreview.Source = null;
                }
            }
        }

        private void customerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (_selectedCustomer != null && customerListView.SelectedItem != _selectedCustomer) {
                btnCancel_Click(sender, e);
            }
            UpdateButtonStates();
        }

        // --- ▼▼▼ 新規追加 (検索・ソート) ▼▼▼ ---

        /// <summary>
        /// 検索ボックスのテキスト変更イベント
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e) {
            string searchTerm = txtSearch.Text;
            LoadCustomers(searchTerm);
        }

        /// <summary>
        /// ソート順切り替えボタンのクリックイベント
        /// </summary>
        private void btnToggleSort_Click(object sender, RoutedEventArgs e) {
            _isSortDescending = !_isSortDescending;
            btnToggleSort.Content = _isSortDescending ? "降順" : "昇順";
            LoadCustomers(txtSearch.Text); // 現在の検索語でリストを再ソート
        }

        // --- ▼▼▼ 新規追加 (郵便番号検索) ▼▼▼ ---

        /// <summary>
        /// 郵便番号入力欄でEnterキーが押された
        /// </summary>
        private void txtZipCode_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                SearchAddressFromZipCode();
            }
        }

        /// <summary>
        /// 「住所検索」ボタンが押された
        /// </summary>
        private void btnSearchZipCode_Click(object sender, RoutedEventArgs e) {
            SearchAddressFromZipCode();
        }

        /// <summary>
        /// 郵便番号APIを叩いて住所を検索・補完する
        /// </summary>
        private async void SearchAddressFromZipCode() {
            string zipCode = txtZipCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(zipCode)) {
                ShowNotification("郵便番号を入力してください");
                return;
            }

            // UIを一時的に無効化
            btnSearchZipCode.IsEnabled = false;
            btnSearchZipCode.Content = "検索中...";
            this.Cursor = Cursors.Wait;

            try {
                // APIリクエスト (例: 1000001)
                HttpResponseMessage response = await _httpClient.GetAsync($"{zipCode}.json");

                if (response.IsSuccessStatusCode) {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // JSONをデシリアライズ
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResponse = JsonSerializer.Deserialize<ZipCodeApiResponse>(jsonResponse, options);

                    if (apiResponse?.Addresses != null && apiResponse.Addresses.Any()) {
                        // 最初の住所情報を取得
                        var address = apiResponse.Addresses.First().Ja;
                        // 住所を結合してTextBoxに設定
                        txtAddress.Text = $"{address.Prefecture}{address.Address1}{address.Address2}{address.Address3}{address.Address4}";
                        ShowNotification("住所を自動入力しました");
                    } else {
                        ShowNotification("該当する住所が見つかりません");
                    }
                } else {
                    ShowNotification($"APIエラー: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx) {
                Debug.WriteLine($"API Request Error: {httpEx.Message}");
                ShowNotification("APIへの接続に失敗しました");
            }
            catch (JsonException jsonEx) {
                Debug.WriteLine($"JSON Parse Error: {jsonEx.Message}");
                ShowNotification("API応答の解析に失敗しました");
            }
            catch (Exception ex) {
                Debug.WriteLine($"Unexpected Error: {ex.Message}");
                MessageBox.Show($"住所検索中に予期せぬエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally {
                // UIを元に戻す
                btnSearchZipCode.IsEnabled = true;
                btnSearchZipCode.Content = "住所検索";
                this.Cursor = Cursors.Arrow;
            }
        }
        // --- ▲▲▲ 新規追加 (郵便番号検索) ▲▲▲ ---

        #endregion

        #region Helper Methods

        private void ClearInputFields() {
            txtName.Clear();
            txtPhone.Clear();
            txtZipCode.Clear(); // 郵便番号欄もクリア
            txtAddress.Clear();
            imgPreview.Source = null;
        }

        private void ToggleEditMode(bool isEditing) {
            if (isEditing) {
                pnlNormalModeButtons.Visibility = Visibility.Collapsed;
                pnlEditModeButtons.Visibility = Visibility.Visible;
                customerListView.IsEnabled = false;
            } else {
                pnlNormalModeButtons.Visibility = Visibility.Visible;
                pnlEditModeButtons.Visibility = Visibility.Collapsed;
                customerListView.IsEnabled = true;
            }
            UpdateButtonStates();
        }

        private void UpdateButtonStates() {
            bool isSelected = customerListView.SelectedItem != null;
            bool isEditing = _selectedCustomer != null;

            // 検索中かどうか (DisplayOrderソートでないか)
            bool isSearching = !string.IsNullOrWhiteSpace(txtSearch.Text);

            if (!isEditing) {
                btnDelete.IsEnabled = isSelected;
                btnEdit.IsEnabled = isSelected;
                // 「▲」「▼」ボタンは、検索中でない(DisplayOrderソートである)時のみ有効
                btnMoveUp.IsEnabled = isSelected && !isSearching;
                btnMoveDown.IsEnabled = isSelected && !isSearching;
            }

            btnRegister.IsEnabled = !isEditing;
        }

        private byte[]? GetImageDataFromPreview() {
            if (imgPreview.Source is BitmapSource bitmapSource) {
                try {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    using (MemoryStream ms = new MemoryStream()) {
                        encoder.Save(ms);
                        return ms.ToArray();
                    }
                }
                catch {
                    return null;
                }
            }
            return null;
        }

        private BitmapImage? LoadImageFromByteArray(byte[]? imageData) {
            if (imageData == null || imageData.Length == 0) return null;

            try {
                var image = new BitmapImage();
                using (var mem = new MemoryStream(imageData)) {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            catch {
                return null;
            }
        }

        #endregion
    }

    // --- ▼▼▼ APIレスポンス用のクラス ▼▼▼ ---
    // (ファイルの下部、または別ファイルに定義)

    public class ZipCodeApiResponse {
        [JsonPropertyName("addresses")]
        public List<AddressData>? Addresses { get; set; }
    }

    public class AddressData {
        [JsonPropertyName("prefectureCode")]
        public string? PrefectureCode { get; set; }

        [JsonPropertyName("ja")]
        public JapaneseAddress? Ja { get; set; }
    }

    public class JapaneseAddress {
        [JsonPropertyName("prefecture")]
        public string? Prefecture { get; set; }

        [JsonPropertyName("address1")]
        public string? Address1 { get; set; } // 市区町村

        [JsonPropertyName("address2")]
        public string? Address2 { get; set; } // ...

        [JsonPropertyName("address3")]
        public string? Address3 { get; set; }

        [JsonPropertyName("address4")]
        public string? Address4 { get; set; }
    }
}