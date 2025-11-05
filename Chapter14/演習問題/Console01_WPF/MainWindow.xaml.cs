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

namespace Exercise01_WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// 「ファイルを選択」ボタンがクリックされたときのイベントハンドラ
        /// </summary>
        private async void SelectFileButton_Click(object sender, RoutedEventArgs e) {
            // 1. ファイル選択ダイアログを表示
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true) {
                string filePath = openFileDialog.FileName;

                // UIを更新して「読み込み中」を通知
                FileContentText.Text = ""; // 前の内容をクリア
                StatusText.Text = "読み込み中...";

                try {
                    // 2. ファイルを非同期で読み込む
                    // UIスレッドをブロックしないように await を使用
                    string fileContent = await ReadTextFileAsync(filePath);

                    // 3. 読み込み完了後、UIを更新
                    FileContentText.Text = fileContent;
                    StatusText.Text = $"読み込み完了: {filePath}";

                }
                catch (Exception ex) {
                    // 4. エラー処理
                    StatusText.Text = "読み込みエラー";
                    MessageBox.Show($"ファイルの読み込み中にエラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 指定されたパスのテキストファイルを非同期で読み込む
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ファイルの内容（文字列）</returns>
        private async Task<string> ReadTextFileAsync(string filePath) {
            // File.ReadAllTextAsync を使うと、バックグラウンドスレッドで
            // ファイルIOが行われ、完了したら結果が返されます。
            // この間、UIスレッドは応答可能な状態を維持します。
            using (StreamReader reader = new StreamReader(filePath)) {
                return await reader.ReadToEndAsync();
            }

            // もしくは、よりシンプルな方法 (ただし、巨大なファイルではメモリを消費します)
            // return await File.ReadAllTextAsync(filePath);
        }
    }
}