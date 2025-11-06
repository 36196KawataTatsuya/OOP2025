using Microsoft.Web.WebView2.Core;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowser {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await WebView.EnsureCoreWebView2Async(null);
            if (WebView.CoreWebView2 != null) {
                WebView.CoreWebView2.Navigate("https://www.google.com");
            }
        }
        // 戻るボタン
        private void BackButton_Click(object sender, RoutedEventArgs e) {
            if (WebView.CanGoBack) {
                WebView.GoBack();
            }
        }

        // 進むボタン
        private void ForwardButton_Click(object sender, RoutedEventArgs e) {
            if (WebView.CanGoForward) {
                WebView.GoForward();
            }
        }

        // 検索実行ボタン
        private void GoButton_Click(object sender, RoutedEventArgs e) {
                
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e) {

        }

        // URLナビゲート処理
        private void NavigateToUrl(string url) {
            // WebView2が初期化されていない場合は無効
            if (WebView == null || WebView.CoreWebView2 == null) {      
                return;
            }

            // URLの形式を確認する
            if (url.StartsWith("http://") || url.StartsWith("https://")) {
                WebView.CoreWebView2.Navigate(url);
            } else {    // デフォルトでhttps://を先頭に付ける
                WebView.CoreWebView2.Navigate("https://" + url);
            }

            try {
                // Uriオブジェクト作成を試行
                Uri uri = new Uri(url);

                // WebViewにナビゲート
                WebView.Source = uri;
            }
            catch (UriFormatException) {
                
            }
        }

        private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {

        }

    }
}