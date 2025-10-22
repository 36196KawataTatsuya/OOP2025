using Microsoft.Win32;
using SQLite;
using System;
using System.Collections.Generic; // List<T>のために必要
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
// using System.Xml.Linq; // ← 不要であれば削除 (未使用のusing)

namespace CustomerApp {
    public partial class MainWindow : Window {
        private readonly string _dbPath;
        private SQLiteConnection _db;

        private ObservableCollection<Customer> _customers;

        private bool _deleteConfirmationRequired = true;

        private Customer? _selectedCustomer = null;

        // ポップアップ通知用のタイマー
        private DispatcherTimer _popupTimer;

        public MainWindow() {
            InitializeComponent();

            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomerDatabase.db");

            InitializeDatabase();

            _customers = new ObservableCollection<Customer>();
            customerListView.ItemsSource = _customers;

            // ポップアップ通知タイマーの初期化
            InitializePopupTimer();

            LoadCustomers();
            UpdateButtonStates();
        }

        /// <summary>
        /// ポップアップ用タイマーを初期化
        /// </summary>
        private void InitializePopupTimer() {
            _popupTimer = new DispatcherTimer();
            // タイマーが止まった時にポップアップを閉じる
            _popupTimer.Tick += (s, e) => {
                notificationPopup.IsOpen = false;
                _popupTimer.Stop();
            };
            // 表示時間は2.5秒に設定
            _popupTimer.Interval = TimeSpan.FromSeconds(2.5);
        }

        /// <summary>
        /// 自動で消える通知を表示する
        /// </summary>
        /// <param name="message">表示するメッセージ</param>
        private void ShowNotification(string message) {
            popupText.Text = message;
            notificationPopup.IsOpen = true; // Popupを表示

            // タイマーを開始
            _popupTimer.Stop();
            _popupTimer.Start();
        }

        private void InitializeDatabase() {
            // データベース接続とテーブル作成
            try {
                _db = new SQLiteConnection(_dbPath);
                _db.CreateTable<Customer>();
            }
            // データベース初期化処理に例外処理を追加
            catch (Exception ex) {
                MessageBox.Show($"データベースの初期化に失敗しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void LoadCustomers() {
            // データベースから顧客データを読み込み、表示用コレクションを更新
            try {
                // DisplayOrderが0のデータを処理するロジック
                var allCustomers = _db.Table<Customer>().ToList();
                var customersWithZeroOrder = allCustomers.Where(c => c.DisplayOrder == 0).ToList();
                bool needsUpdate = customersWithZeroOrder.Any();
                if (needsUpdate) {
                    int maxOrder = allCustomers.Where(c => c.DisplayOrder > 0)
                                              .Select(c => c.DisplayOrder)
                                              .DefaultIfEmpty(0).Max();
                    foreach (var customer in customersWithZeroOrder.OrderBy(c => c.Id)) {
                        maxOrder++;
                        customer.DisplayOrder = maxOrder;
                        _db.Update(customer);
                    }
                    allCustomers = _db.Table<Customer>().ToList(); // 再読み込み
                }

                // DisplayOrderでソート
                var sortedCustomers = allCustomers.OrderBy(c => c.DisplayOrder).ToList();

                _customers.Clear();
                foreach (var customer in sortedCustomers) {
                    _customers.Add(customer);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"データ読み込みエラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        private void btnRegister_Click(object sender, RoutedEventArgs e) {
            // 氏名と電話番号の必須チェック
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
                // DBエラーは重要なため、MessageBoxのままに設定
                MessageBox.Show($"登録エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) {
                ShowNotification("削除する顧客を選択してください");
                return;
            }

            // 削除は重要な操作なため、MessageBoxのままに設定
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
                LoadCustomers();
                ClearInputFields();
            }
            catch (Exception ex) {
                MessageBox.Show($"削除エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 「編集」ボタンクリック
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            if (customerListView.SelectedItem is not Customer selectedCustomer) {
                ShowNotification("編集する顧客を選択してください");
                return;
            }

            _selectedCustomer = selectedCustomer;

            txtName.Text = _selectedCustomer.Name;
            txtPhone.Text = _selectedCustomer.Phone;
            txtAddress.Text = _selectedCustomer.Address;
            imgPreview.Source = LoadImageFromByteArray(_selectedCustomer.Picture);

            ToggleEditMode(true);
        }

        /// <summary>
        /// 「再登録」ボタンクリック(編集モード時)
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            if (_selectedCustomer == null) return;

            // 氏名と電話番号の必須チェック
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

                LoadCustomers();
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

                    // 選択が外れないよう、IDを一時的に保持
                    int selectedId = selectedCustomer.Id;
                    LoadCustomers(); // リスト再読み込み

                    // 保持していたIDを使って、新しいリストから項目を再選択する
                    var customerToReselect = _customers.FirstOrDefault(c => c.Id == selectedId);
                    if (customerToReselect != null) {
                        customerListView.SelectedItem = customerToReselect;
                        customerListView.ScrollIntoView(customerToReselect);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"順序変更エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    // エラーが起きたら元に戻す (念のため)
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

                    // 選択が外れないよう、IDを一時的に保持
                    int selectedId = selectedCustomer.Id;
                    LoadCustomers(); // リスト再読み込み

                    // 保持していたIDを使って、新しいリストから項目を再選択する
                    var customerToReselect = _customers.FirstOrDefault(c => c.Id == selectedId);
                    if (customerToReselect != null) {
                        customerListView.SelectedItem = customerToReselect;
                        customerListView.ScrollIntoView(customerToReselect);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"順序変更エラー: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    // エラーが起きたら元に戻す (念のため)
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
            // 編集モード中に選択が変わったらキャンセル
            if (_selectedCustomer != null && customerListView.SelectedItem != _selectedCustomer) {
                btnCancel_Click(sender, e);
            }
            UpdateButtonStates();
        }

        #endregion

        // --- ヘルパーメソッド ---
        #region Helper Methods

        private void ClearInputFields() {
            txtName.Clear();
            txtPhone.Clear();
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

            if (!isEditing) {
                btnDelete.IsEnabled = isSelected;
                btnEdit.IsEnabled = isSelected;
                btnMoveUp.IsEnabled = isSelected;
                btnMoveDown.IsEnabled = isSelected;
            }
            // 編集中は通常ボタンは無効、編集用ボタンは常に有効
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
}