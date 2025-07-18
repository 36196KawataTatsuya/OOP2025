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
                MessageBox.Show("URL�����������͂���Ă��܂���\n�e�L�X�g�{�b�N�X���m�F���Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (!Uri.TryCreate(tbUrl.Text, UriKind.Absolute, out Uri? result)) {
                MessageBox.Show("���͂��ꂽ������͗L����URL�ł͂���܂���\n������URL�̌`���œ��͂��Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var wc = new WebClient()) {
                var url = wc.OpenRead(tbUrl.Text);
                XDocument xdoc = XDocument.Load(url); //RSS�̎擾

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
