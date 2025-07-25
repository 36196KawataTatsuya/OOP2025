using System.Net;
using System.Security.Policy;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items = new List<ItemData>();
        private bool webView2Init = false;

        //RSS�̃f�t�H���gURL��ۑ�
        private readonly Dictionary<string, string> _defaultUrlDictionary = new Dictionary<string, string>
{
    { "��v", "https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
    { "����", "https://news.yahoo.co.jp/rss/topics/domestic.xml" },
    { "����", "https://news.yahoo.co.jp/rss/topics/world.xml" },
    { "�o��", "https://news.yahoo.co.jp/rss/topics/business.xml" },
    { "�G���^��", "https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
    { "�X�|�[�c", "https://news.yahoo.co.jp/rss/topics/sports.xml" },
    { "IT", "https://news.yahoo.co.jp/rss/topics/it.xml" },
    { "�Ȋw", "https://news.yahoo.co.jp/rss/topics/science.xml" },
    { "�n��", "https://news.yahoo.co.jp/rss/topics/local.xml" }
};
        //���C�ɓ���o�^��ۑ����鎫���̎��O�쐬
        private Dictionary<string, string> _favoriteUrlDictionary = new Dictionary<string, string>();
        //�ݒ�t�@�C���� (�m���)
        private const string SettingsFileName = "rssSetting.json";

        public Form1() {
            InitializeComponent();
            InitializeComboBox();
            rollback.Enabled = false;
            forward.Enabled = false;
        }

        //�R���{�{�b�N�X�ނ�����������
        private void InitializeComboBox() {
            //���C�ɓ���ǂݍ���
            LoadFavorites();
            //ComboBox�̏�����
            UpdateComboBoxes();
            //URL�R���{�{�b�N�X���I����Ԃɂ���
            urlComboBox.SelectedIndex = -1;
        }

        //�ݒ�t�@�C�����炨�C�ɓ����ǂݍ���
        private void LoadFavorites() {
            //�t�@�C��������Γǂݍ���
            if (File.Exists(SettingsFileName)) {
                //JSON�t�@�C����ǂ�Ŏ����ɕϊ�
                try {
                    string json = File.ReadAllText(SettingsFileName);
                    _favoriteUrlDictionary = JsonSerializer
                        .Deserialize<Dictionary<string, string>>(json)
                        ?? new Dictionary<string, string>();
                }
                //���s���ɃG���[���b�Z�[�W��\��
                catch (Exception e) {
                    MessageBox
                        .Show($"�ݒ�t�@�C�����ǂݍ��܂�܂���ł���\n{e.Message}"
                        , "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _favoriteUrlDictionary = new Dictionary<string, string>();
                }
            }
        }

        //�R���{�{�b�N�X�̍X�V
        private void UpdateComboBoxes() {
            //urlComboBox�̍X�V
            urlComboBox.DataSource = null;

            //�f�t�H���g��URL�����Ƃ��C�ɓ���URL�������������A���X�g�ɂ���
            var binedList = _defaultUrlDictionary
                .Concat(_favoriteUrlDictionary).ToList();
            //��L�Ō����������X�g��ComboBox�Ƀo�C���h
            urlComboBox.DataSource = binedList;
            urlComboBox.DisplayMember = "Key";
            urlComboBox.ValueMember = "Value";

            // favoriteName�R���{�{�b�N�X�̍X�V
            favoriteName.DataSource = null;

            //favoriteName�ɖ��O��\�����邽�߂̃��X�g���쐬
            var favoriteList = _favoriteUrlDictionary.ToList();
            favoriteName.DataSource = favoriteList;

            //�f�[�^��1�ȏ�o�^����Ă��鎞�̂݁ADisplayMember��ValueMember��ݒ�
            if (favoriteList.Count > 0) {
                favoriteName.DisplayMember = "Key";
                favoriteName.ValueMember = "Value";
            }

            //�S�Ă̑I����Ԃ��N���A
            urlComboBox.SelectedIndex = -1;
            urlComboBox.Text = string.Empty;
            favoriteName.SelectedIndex = -1;
            favoriteName.Text = string.Empty;
        }

        //RSS���擾���鎞�̃C�x���g�n���h��
        private async void btRssGet_Click(object sender, EventArgs e) {
            //URL�R���{�{�b�N�X����I���������̂�URL���擾
            string? url = urlComboBox.SelectedValue as string ?? urlComboBox.Text;
            //�ʏ���͂̏ꍇ�̗�O���������{
            if (string.IsNullOrWhiteSpace(urlComboBox.Text)) {
                MessageBox.Show("URL�����������͂���Ă��܂���\n" +
                    "�e�L�X�g�{�b�N�X���m�F���Ă�������", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? result)) {
                MessageBox.Show("���͂��ꂽ������͗L����URL�ł͂���܂���\n" +
                    "������URL�̌`���œ��͂��Ă�������\n" + url, "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var hc = new HttpClient()) {

                //xml���擾 (HTTP�N���C�A���g�쐬)
                string xml = await hc.GetStringAsync(url);
                XDocument xdoc = XDocument.Parse(xml); //RSS�̎擾

                //�A�C�e���f�[�^���擾
                items = xdoc.Root!.Descendants("item")
                    .Select(x => new ItemData {
                        Title = (string?)x.Element("title") ?? "No Title",
                        Link = (string?)x.Element("link") ?? "No Link"
                    }).ToList();
            }
            //web�r���[�A��������
            webView2Init = true;
            lbTitles.Items.Clear();
            lbTitles.Items.AddRange(items.ToArray());
            lbTitles.DisplayMember = "Title";

        }

        //���X�g�{�b�N�X����WebView2��URL��n��
        private void lbTitles_Click(object sender, EventArgs e) {
            //���X�g�{�b�N�X�̑I�������A�C�e�����烊���N���擾
            string? linkUrl = null;
            if (lbTitles.SelectedItem is ItemData selectedItem) {
                linkUrl = selectedItem.Link;
            }
            //����Ƀ����N���擾�ł����ꍇ��WebView2�ɕ\��
            if (!string.IsNullOrWhiteSpace(linkUrl) && Uri
                .TryCreate(linkUrl, UriKind.Absolute, out Uri? uriResult)) {
                wvRssLink.Source = uriResult;
            }
        }

        //���C�ɓ���o�^�̏���
        private void favoriteAdd_Click(object sender, EventArgs e) {
            string url = urlComboBox.Text;
            string name = favoriteName.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url)) {
                MessageBox.Show("���C�ɓ��薼�̂�URL�̗�������͂��Ă�������"
                    , "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("�L����URL�ł͂���܂���", "�G���["
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_defaultUrlDictionary.ContainsKey(name)
                || _favoriteUrlDictionary.ContainsKey(name)) {
                MessageBox.Show("���̖��̂͊��Ɏg�p����Ă��܂�", "�G���["
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ���C�ɓ���ɒǉ�
            _favoriteUrlDictionary.Add(name, url);
            SaveFavorites();
            UpdateComboBoxes();

            MessageBox.Show($"�u{name}�v�����C�ɓ���ɒǉ����܂����B"
                , "�ǉ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ���C�ɓ�����t�@�C���ɕۑ�����
        private void SaveFavorites() {
            //�ݒ�t�@�C���ɕۑ�����
            try {
                string json = JsonSerializer
                    .Serialize(_favoriteUrlDictionary
                    , new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFileName, json);
            }
            catch (Exception e) {
                MessageBox.Show($"�ݒ�t�@�C���̕ۑ��Ɏ��s���܂���\n{e.Message}"
                    , "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //���C�ɓ���폜�̏���
        private void favoriteRemove_Click(object sender, EventArgs e) {
            //�I���ł��Ă��邩���`�F�b�N
            if (favoriteName.SelectedItem == null) {
                MessageBox.Show("�폜���邨�C�ɓ�����ꗗ����I�����Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //�I�����ꂽ�A�C�e���̃L�[���擾
            string selectedKey =
                ((KeyValuePair<string, string>)favoriteName.SelectedItem).Key;

            //�I���A�C�e���̊m�F�_�C�A���O��\��
            var result = MessageBox.Show($"�u{selectedKey}�v���폜���܂����H"
                , "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                //�폜�����s
                _favoriteUrlDictionary.Remove(selectedKey);
                //�ύX��ۑ����čX�V
                SaveFavorites();
                UpdateComboBoxes();

                MessageBox.Show("�폜���܂���"
                    , "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //�i�ށE�߂�{�^���̃C�x���g�n���h��
        private void wvRssLink_SourceChanged(object sender
            , Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
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

        //urlComboBox�̓��͓��e�N���A
        private void urlClear_Click(object sender, EventArgs e) {
            urlComboBox.Text = string.Empty;
        }

        //�ȉ��AWebView2�̏ڍׂȃC�x���g�n���h��
        private void rollback_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }
        private void forward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }
        private void reflesh_Click(object sender, EventArgs e) {
            if (webView2Init == false) {
                MessageBox.Show
                    ("RSS���擾���Ă��烊�t���b�V�����Ă�������", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                wvRssLink.Reload();
            }
        }

    }
}
