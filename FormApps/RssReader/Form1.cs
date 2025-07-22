using System.Net;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
            rollback.Enabled = false;
            forward.Enabled = false;
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(urlComboBox.Text)) {
                MessageBox.Show("URLが正しく入力されていません\nテキストボックスを確認してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (!Uri.TryCreate(urlComboBox.Text, UriKind.Absolute, out Uri? result)) {
                MessageBox.Show("入力された文字列は有効なURLではありません\n正しいURLの形式で入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(urlComboBox.Text);
                XDocument xdoc = XDocument.Parse(xml); //RSSの取得

                items = xdoc.Root!.Descendants("item")
                    .Select(x => new ItemData {
                        Title = (string?)x.Element("title") ?? "No Title",
                        Link = (string?)x.Element("link") ?? "No Link"
                    }).ToList();
            }
            lbTitles.Items.Clear();
            lbTitles.Items.AddRange(items.ToArray());
            lbTitles.DisplayMember = "Title";

        }


        private void lbTitles_Click(object sender, EventArgs e) {
            string? linkUrl = null;
            if (lbTitles.SelectedItem is ItemData selectedItem) {
                linkUrl = selectedItem.Link;
            }
            if (!string.IsNullOrWhiteSpace(linkUrl) && Uri.TryCreate(linkUrl, UriKind.Absolute, out Uri? uriResult)) {
                wvRssLink.Source = uriResult;
            }   
        }

        private void favoriteAdd_Click(object sender, EventArgs e) {
            
        }

        private void favoriteRemove_Click(object sender, EventArgs e) {

        }

        private void urlSelect_Click(object sender, EventArgs e) {

        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            if (wvRssLink.CanGoBack) {
                rollback.Enabled = true;
            } else {
                rollback.Enabled = false;
            }
            if (wvRssLink.CanGoForward) {
                forward.Enabled = true;
            } else {
                forward.Enabled = false;
            }
        }

        private void urlClear_Click(object sender, EventArgs e) {
            urlComboBox.Text = string.Empty;
        }
        private void rollback_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }
        private void forward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }
        private void reflesh_Click(object sender, EventArgs e) {
            wvRssLink.Reload();
        }

    }
}
