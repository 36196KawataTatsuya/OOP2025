using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {
        private List<ItemData> items = new List<ItemData>();
        private bool webView2Init = false;

        private AppSettings _currentSettings = new AppSettings();

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

        //�ݒ�t�@�C����
        private const string SettingsFileName = "rssSetting.json";

        public Form1() {
            InitializeComponent();
            InitializeComboBox();
            rollback.Enabled = false;
            forward.Enabled = false;

            this.FormClosing += (s, e) => SaveSettings();
        }

        //�R���{�{�b�N�X�ނ�����������
        private void InitializeComboBox() {
            LoadSettings();
            UpdateComboBoxes();
            urlComboBox.SelectedIndex = -1;
        }

        //�ݒ�t�@�C�����炨�C�ɓ���Ɛݒ��ǂݍ���
        private void LoadSettings() {
            if (File.Exists(SettingsFileName)) {
                try {
                    string json = File.ReadAllText(SettingsFileName);
                    _currentSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
                catch (Exception e) {
                    MessageBox.Show($"�ݒ�t�@�C�����ǂݍ��܂�܂���ł���\n{e.Message}", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _currentSettings = new AppSettings();
                }
            }
            // �ǂݍ��񂾐ݒ��K�p
            this.BackColor = ColorTranslator.FromHtml(_currentSettings.BackgroundColorHtml);
        }

        // ���C�ɓ���Ɛݒ���t�@�C���ɕۑ�����
        private void SaveSettings() {
            try {
                _currentSettings.BackgroundColorHtml = ColorTranslator.ToHtml(this.BackColor);
                string json = JsonSerializer.Serialize(_currentSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFileName, json);
            }
            catch (Exception e) {
                MessageBox.Show($"�ݒ�t�@�C���̕ۑ��Ɏ��s���܂���\n{e.Message}", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //�R���{�{�b�N�X�̍X�V
        private void UpdateComboBoxes() {
            urlComboBox.DataSource = null;
            var combinedList = _defaultUrlDictionary.Concat(_currentSettings.FavoriteUrls).ToList();
            urlComboBox.DataSource = combinedList;
            urlComboBox.DisplayMember = "Key";
            urlComboBox.ValueMember = "Value";

            favoriteName.DataSource = null;
            var favoriteList = _currentSettings.FavoriteUrls.ToList();
            favoriteName.DataSource = favoriteList;

            if (favoriteList.Count > 0) {
                favoriteName.DisplayMember = "Key";
                favoriteName.ValueMember = "Value";
            }

            urlComboBox.SelectedIndex = -1;
            urlComboBox.Text = string.Empty;
            favoriteName.SelectedIndex = -1;
            favoriteName.Text = string.Empty;
        }

        //RSS���擾���鎞�̃C�x���g�n���h��
        private async void btRssGet_Click(object sender, EventArgs e) {
            string? url = urlComboBox.SelectedValue as string ?? urlComboBox.Text;
            if (string.IsNullOrWhiteSpace(urlComboBox.Text)) {
                MessageBox.Show("URL�����������͂���Ă��܂���\n" + "�e�L�X�g�{�b�N�X���m�F���Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("���͂��ꂽ������͗L����URL�ł͂���܂���\n" + "������URL�̌`���œ��͂��Ă�������\n" + url, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var hc = new HttpClient()) {
                string xml = await hc.GetStringAsync(url);
                XDocument xdoc = XDocument.Parse(xml);
                items = xdoc.Root!.Descendants("item")
                    .Select(x => new ItemData {
                        Title = (string?)x.Element("title") ?? "No Title",
                        Link = (string?)x.Element("link") ?? "No Link"
                    }).ToList();
            }
            webView2Init = true;
            lbTitles.Items.Clear();
            lbTitles.Items.AddRange(items.ToArray());
            lbTitles.DisplayMember = "Title";
        }

        //���X�g�{�b�N�X����WebView2��URL��n��
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedItem is ItemData selectedItem) {
                string? linkUrl = selectedItem.Link;
                if (!string.IsNullOrWhiteSpace(linkUrl) && Uri.TryCreate(linkUrl, UriKind.Absolute, out Uri? uriResult)) {
                    wvRssLink.Source = uriResult;
                }
            }
        }

        //���C�ɓ���o�^�̏���
        private void favoriteAdd_Click(object sender, EventArgs e) {
            string url = urlComboBox.Text;
            string name = favoriteName.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url)) {
                MessageBox.Show("���C�ɓ��薼�̂�URL�̗�������͂��Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("�L����URL�ł͂���܂���", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_defaultUrlDictionary.ContainsKey(name) || _currentSettings.FavoriteUrls.ContainsKey(name)) {
                MessageBox.Show("���̖��̂͊��Ɏg�p����Ă��܂�", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentSettings.FavoriteUrls.Add(name, url);
            SaveSettings();
            UpdateComboBoxes();

            MessageBox.Show($"�u{name}�v�����C�ɓ���ɒǉ����܂����B", "�ǉ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //���C�ɓ���폜�̏���
        private void favoriteRemove_Click(object sender, EventArgs e) {
            if (favoriteName.SelectedItem == null) {
                MessageBox.Show("�폜���邨�C�ɓ�����ꗗ����I�����Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedKey = ((KeyValuePair<string, string>)favoriteName.SelectedItem).Key;

            var result = MessageBox.Show($"�u{selectedKey}�v���폜���܂����H", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                _currentSettings.FavoriteUrls.Remove(selectedKey);
                SaveSettings();
                UpdateComboBoxes();
                MessageBox.Show("�폜���܂���", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //�i�ށE�߂�{�^���̃C�x���g�n���h��
        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            rollback.Enabled = wvRssLink.CanGoBack;
            forward.Enabled = wvRssLink.CanGoForward;
        }

        //urlComboBox�̓��͓��e�N���A
        private void urlClear_Click(object sender, EventArgs e) {
            urlComboBox.Text = string.Empty;
        }

        private void rollback_Click(object sender, EventArgs e) => wvRssLink.GoBack();
        private void forward_Click(object sender, EventArgs e) => wvRssLink.GoForward();
        private void reflesh_Click(object sender, EventArgs e) {
            if (webView2Init) {
                wvRssLink.Reload();
            } else {
                MessageBox.Show("RSS���擾���Ă��烊�t���b�V�����Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region ���j���[�C�x���g�n���h��
        // �ݒ�t�@�C�����C���|�[�g����
        private void �C���|�[�gToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "�ݒ�t�@�C�� (*.json)|*.json|���ׂẴt�@�C�� (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    try {
                        string json = File.ReadAllText(ofd.FileName);
                        _currentSettings = JsonSerializer
                            .Deserialize<AppSettings>(json) ?? new AppSettings();
                        this.BackColor = ColorTranslator
                            .FromHtml(_currentSettings.BackgroundColorHtml);
                        UpdateComboBoxes();
                        MessageBox.Show("�ݒ���C���|�[�g���܂���"
                            , "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"�C���|�[�g�Ɏ��s���܂���\n{ex.Message}"
                            , "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ���݂̐ݒ���G�N�X�|�[�g
        private void �G�N�X�|�[�gToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var sfd = new SaveFileDialog()) {
                sfd.Filter = "�ݒ�t�@�C�� (*.json)|*.json";
                sfd.FileName = "rssSetting.json";
                if (sfd.ShowDialog() == DialogResult.OK) {
                    try {
                        _currentSettings.BackgroundColorHtml =
                            ColorTranslator.ToHtml(this.BackColor);
                        string json = JsonSerializer
                            .Serialize(_currentSettings
                            , new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(sfd.FileName, json);
                        MessageBox.Show("�ݒ���G�N�X�|�[�g���܂���"
                            , "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"�G�N�X�|�[�g�Ɏ��s���܂���\n{ex.Message}"
                            , "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // �w�i�F��ύX����
        private void �w�i�F�ݒ�ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var cd = new ColorDialog()) {
                cd.Color = this.BackColor;
                if (cd.ShowDialog() == DialogResult.OK) {
                    this.BackColor = cd.Color;
                }
            }
        }

        // �A�v���P�[�V�������I��
        private void �I��ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close(); // FormClosing�C�x���g�ŕۑ������
        }

        // ���͗��̃e�L�X�g���N���A
        private void ���͗����N���AToolStripMenuItem_Click(object sender, EventArgs e) {
            urlComboBox.SelectedIndex = -1;
            urlComboBox.Text = string.Empty;
            favoriteName.SelectedIndex = -1;
            favoriteName.Text = string.Empty;
        }

        // ���ׂĂ̂��C�ɓ�����폜
        private void ���C�ɓ����S�č폜ToolStripMenuItem_Click(object sender, EventArgs e) {
            var result = MessageBox
                .Show("�{���ɂ��C�ɓ����S�č폜���Ă���낵���ł����H"
                , "�ŏI�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                _currentSettings.FavoriteUrls.Clear();
                SaveSettings();
                UpdateComboBoxes();
                MessageBox.Show("���ׂĂ̂��C�ɓ�����폜���܂���"
                    , "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // �w���v�i�戵�������j��\������
        private void �w���vToolStripMenuItem1_Click(object? sender, EventArgs e) {
            string helpText =
                "RSS���[�_�[ �A�v���P�[�V�����̎g����\r\n" +
                "====================================\r\n" +
                "\r\n" +
                "�� ��{�I�Ȏg����\r\n" +
                "1. ��ʏ㕔�̃v���_�E�����j���[����ǂ݂����j���[�X�̃J�e�S���i��v�A�����Ȃǁj��I�����܂��B\r\n" +
                "   �܂��́A���ڃe�L�X�g�{�b�N�X��RSS�t�B�[�h��URL����͂��邱�Ƃ��ł��܂��B\r\n" +
                "2. �u�f�[�^�擾�v�{�^�����N���b�N���܂��B\r\n" +
                "3. ��ʒ��i�̃��X�g�ɋL���̃^�C�g���ꗗ���\������܂��B\r\n" +
                "4. �^�C�g�����N���b�N����ƁA��ʉ����̃u���E�U�̈��Web�y�[�W���\������܂��B\r\n" +
                "5. �u���E�U�̈�̏�ɂ���u�߂�v�u�i�ށv�u�ēǂݍ��݁v�{�^���ő��삪�\�ł��B\r\n" +
                "\r\n" +
                "�� ���C�ɓ���@�\\r\n" +
                "�y�o�^���@�z\r\n" +
                "1. �㕔��URL���͗��ɁA�o�^������RSS�t�B�[�h��URL����͂��܂��B\r\n" +
                "2. �u���C�ɓ��薼�́v���͗��ɁA������₷�����O����͂��܂��B\r\n" +
                "3. �u�o�^�v�{�^�����N���b�N����Ƃ��C�ɓ���ɒǉ�����܂��B\r\n" +
                "\r\n" +
                "�y�g�����z\r\n" +
                "�EURL���͗��̃v���_�E�����j���[����A�o�^�������C�ɓ���̖��O��I�����āu�f�[�^�擾�v�{�^���������܂��B\r\n" +
                "\r\n" +
                "�y�폜���@�z\r\n" +
                "1. �u���C�ɓ��薼�́v�̃v���_�E�����j���[����A�폜���������C�ɓ���̖��O��I�����܂��B\r\n" +
                "2. �u�o�^�����v�{�^�����N���b�N���܂��B\r\n" +
                "\r\n" +
                "�� ���j���[�o�[�̋@�\\r\n" +
                "�y�t�@�C���z\r\n" +
                "�E���C�ɓ���ݒ� > �C���|�[�g: �ݒ�t�@�C��(rssSetting.json)��ǂݍ��݁A���C�ɓ���Ɣw�i�F�𕜌����܂��B\r\n" +
                "�E���C�ɓ���ݒ� > �G�N�X�|�[�g: ���݂̂��C�ɓ���Ɣw�i�F�̐ݒ���t�@�C���ɕۑ����܂��B\r\n" +
                "�E�w�i�F�ݒ�: �A�v���P�[�V�����S�̂̔w�i�F�����R�ɕύX�ł��܂��B\r\n" +
                "�E�I��: �A�v���P�[�V�������I�����܂��B�ݒ�͎����I�ɕۑ�����܂��B\r\n" +
                "\r\n" +
                "�y�ҏW�z\r\n" +
                "�E���͗����N���A: URL�Ƃ��C�ɓ��薼�̂̓��͗�����ɂ��܂��B\r\n" +
                "�E���C�ɓ����S�č폜: �o�^�������ׂĂ̂��C�ɓ�����A�m�F�̂̂��ɍ폜���܂��B\r\n" +
                "\r\n" +
                "�y�w���v�z\r\n" +
                "�E�w���v: ���̎g����������\�����܂��B\r\n" +
                "�E�o�[�W�������: �A�v���P�[�V�����̃o�[�W��������\�����܂��B\r\n" +
                "\r\n" +
                "�� �ݒ�̕ۑ��ɂ���\r\n" +
                "�E���C�ɓ�����Ɣw�i�F�̐ݒ�́A�A�v���P�[�V�����I�����Ɏ��s�t�@�C���Ɠ����t�H���_��`rssSetting.json`�Ƃ��Ď����I�ɕۑ�����A����N�����ɓǂݍ��܂�܂��B\r\n";

            using (var helpForm = new Form()) {
                helpForm.Text = "�w���v - �g����";
                helpForm.StartPosition = FormStartPosition.CenterParent;
                helpForm.Size = new Size(600, 500);

                var textBox = new TextBox {
                    Text = helpText,
                    Multiline = true,
                    ReadOnly = true,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular),
                    BackColor = SystemColors.Window,
                    ForeColor = SystemColors.WindowText
                };

                helpForm.Controls.Add(textBox);
                helpForm.ShowDialog(this);
            }
        }

        // �o�[�W��������\��
        private void �o�[�W�������ToolStripMenuItem_Click(object sender, EventArgs e) {
            string appName = "RSS Reader";
            string version = "1.025.7.25.3";
            MessageBox.Show($"{appName}\n�o�[�W����: {version}"
                , "�o�[�W�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

    }
}