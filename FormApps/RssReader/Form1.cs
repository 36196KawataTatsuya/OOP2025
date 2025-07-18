using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private void btRssGet_Click(object sender, EventArgs e) {
            if (tbUrl.Text == "") {
                MessageBox.Show("URLが正しく入力されていません\nテキストボックスを確認してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (!Uri.TryCreate(tbUrl.Text, UriKind.Absolute, out Uri? result)) {
                MessageBox.Show("入力された文字列は有効なURLではありません\n正しいURLの形式で入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var wc = new WebClient()) {
                var url = wc.OpenRead(tbUrl.Text);
                XDocument xdoc = XDocument.Load(url); //RSSの取得

                items = xdoc.Root!.Descendants("item")
                    .Select(x => new ItemData {
                        Title = (string?)x.Element("title") ?? (string?)x.Element("Title") ?? "No Title"
                    }).ToList();
            }
            lbTitles.Items.Clear();
            //foreach (var item in items) {
            //    lbTitles.Items.Add(item.Title);
            //}
            items.ForEach(x => lbTitles.Items.Add(x.Title));
            
        }


    }
}
