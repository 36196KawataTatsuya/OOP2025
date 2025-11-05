using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Threading.Tasks; // Taskのために必要
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Console01_WPF {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// 「ファイルを選択」ボタンがクリックされたときのイベントハンドラ
        /// </summary>
        private async void SelectFileButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true) {
                string filePath = openFileDialog.FileName;

                // UIを更新して「読み込み中」を通知
                FileContentText.Text = "";
                StatusText.Text = "読み込み中...";
                SelectFileButton.IsEnabled = false;

                try {
                    // 2. ファイルを非同期で読み込むタスクを開始
                    Task<string> readTask = ReadTextFileAsync(filePath);

                    // 3. テストモードの待機タスクを準備
                    Task delayTask = Task.CompletedTask;
                    if (TestModeCheckBox.IsChecked == true) {
                        StatusText.Text = "読み込み中... (テストモードで5秒待機します)";
                        // 5秒待機するタスクをセット
                        delayTask = Task.Delay(5000);
                    }

                    // 4. 両方のタスク（読み込み と 待機）が完了するのを待つ
                    await Task.WhenAll(readTask, delayTask);

                    // 5. 読み込み完了後、UIを更新
                    string fileContent = await readTask;
                    FileContentText.Text = fileContent;
                    StatusText.Text = $"読み込み完了: {filePath}";

                }
                catch (Exception ex) {
                    // 6. エラー処理
                    StatusText.Text = "読み込みエラー";
                    MessageBox.Show($"ファイルの読み込み中にエラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally {
                    SelectFileButton.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// 指定されたパスのテキストファイルを非同期で読み込む
        /// </summary>
        private async Task<string> ReadTextFileAsync(string filePath) {
            using (StreamReader reader = new StreamReader(filePath)) {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// 【NEW】UI応答テスト用ボタンのクリックイベント
        /// </summary>
        private void UiTestButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("UIは応答しています！", "UIテスト");
        }
    }
}