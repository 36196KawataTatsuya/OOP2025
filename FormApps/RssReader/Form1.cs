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

        //RSSのデフォルトURLを保存
        private readonly Dictionary<string, string> _defaultUrlDictionary = new Dictionary<string, string>
        {
            { "主要", "https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            { "国内", "https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            { "国際", "https://news.yahoo.co.jp/rss/topics/world.xml" },
            { "経済", "https://news.yahoo.co.jp/rss/topics/business.xml" },
            { "エンタメ", "https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            { "スポーツ", "https://news.yahoo.co.jp/rss/topics/sports.xml" },
            { "IT", "https://news.yahoo.co.jp/rss/topics/it.xml" },
            { "科学", "https://news.yahoo.co.jp/rss/topics/science.xml" },
            { "地域", "https://news.yahoo.co.jp/rss/topics/local.xml" }
        };

        //設定ファイル名
        private const string SettingsFileName = "rssSetting.json";

        public Form1() {
            InitializeComponent();
            InitializeComboBox();
            rollback.Enabled = false;
            forward.Enabled = false;

            this.FormClosing += (s, e) => SaveSettings();
        }

        //コンボボックス類を初期化する
        private void InitializeComboBox() {
            LoadSettings();
            UpdateComboBoxes();
            urlComboBox.SelectedIndex = -1;
        }

        //設定ファイルからお気に入りと設定を読み込む
        private void LoadSettings() {
            if (File.Exists(SettingsFileName)) {
                try {
                    string json = File.ReadAllText(SettingsFileName);
                    _currentSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
                catch (Exception e) {
                    MessageBox.Show($"設定ファイルが読み込まれませんでした\n{e.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _currentSettings = new AppSettings();
                }
            }
            // 読み込んだ設定を適用
            this.BackColor = ColorTranslator.FromHtml(_currentSettings.BackgroundColorHtml);
        }

        // お気に入りと設定をファイルに保存する
        private void SaveSettings() {
            try {
                _currentSettings.BackgroundColorHtml = ColorTranslator.ToHtml(this.BackColor);
                string json = JsonSerializer.Serialize(_currentSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFileName, json);
            }
            catch (Exception e) {
                MessageBox.Show($"設定ファイルの保存に失敗しました\n{e.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //コンボボックスの更新
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

        //RSSを取得する時のイベントハンドラ
        private async void btRssGet_Click(object sender, EventArgs e) {
            string? url = urlComboBox.SelectedValue as string ?? urlComboBox.Text;
            if (string.IsNullOrWhiteSpace(urlComboBox.Text)) {
                MessageBox.Show("URLが正しく入力されていません\n" + "テキストボックスを確認してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("入力された文字列は有効なURLではありません\n" + "正しいURLの形式で入力してください\n" + url, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //リストボックスからWebView2にURLを渡す
        private void lbTitles_Click(object sender, EventArgs e) {
            if (lbTitles.SelectedItem is ItemData selectedItem) {
                string? linkUrl = selectedItem.Link;
                if (!string.IsNullOrWhiteSpace(linkUrl) && Uri.TryCreate(linkUrl, UriKind.Absolute, out Uri? uriResult)) {
                    wvRssLink.Source = uriResult;
                }
            }
        }

        //お気に入り登録の処理
        private void favoriteAdd_Click(object sender, EventArgs e) {
            string url = urlComboBox.Text;
            string name = favoriteName.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url)) {
                MessageBox.Show("お気に入り名称とURLの両方を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("有効なURLではありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_defaultUrlDictionary.ContainsKey(name) || _currentSettings.FavoriteUrls.ContainsKey(name)) {
                MessageBox.Show("その名称は既に使用されています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentSettings.FavoriteUrls.Add(name, url);
            SaveSettings();
            UpdateComboBoxes();

            MessageBox.Show($"「{name}」をお気に入りに追加しました。", "追加完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //お気に入り削除の処理
        private void favoriteRemove_Click(object sender, EventArgs e) {
            if (favoriteName.SelectedItem == null) {
                MessageBox.Show("削除するお気に入りを一覧から選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedKey = ((KeyValuePair<string, string>)favoriteName.SelectedItem).Key;

            var result = MessageBox.Show($"「{selectedKey}」を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                _currentSettings.FavoriteUrls.Remove(selectedKey);
                SaveSettings();
                UpdateComboBoxes();
                MessageBox.Show("削除しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //進む・戻るボタンのイベントハンドラ
        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            rollback.Enabled = wvRssLink.CanGoBack;
            forward.Enabled = wvRssLink.CanGoForward;
        }

        //urlComboBoxの入力内容クリア
        private void urlClear_Click(object sender, EventArgs e) {
            urlComboBox.Text = string.Empty;
        }

        private void rollback_Click(object sender, EventArgs e) => wvRssLink.GoBack();
        private void forward_Click(object sender, EventArgs e) => wvRssLink.GoForward();
        private void reflesh_Click(object sender, EventArgs e) {
            if (webView2Init) {
                wvRssLink.Reload();
            } else {
                MessageBox.Show("RSSを取得してからリフレッシュしてください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region メニューイベントハンドラ
        // 設定ファイルをインポートする
        private void インポートToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var ofd = new OpenFileDialog()) {
                ofd.Filter = "設定ファイル (*.json)|*.json|すべてのファイル (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    try {
                        string json = File.ReadAllText(ofd.FileName);
                        _currentSettings = JsonSerializer
                            .Deserialize<AppSettings>(json) ?? new AppSettings();
                        this.BackColor = ColorTranslator
                            .FromHtml(_currentSettings.BackgroundColorHtml);
                        UpdateComboBoxes();
                        MessageBox.Show("設定をインポートしました"
                            , "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"インポートに失敗しました\n{ex.Message}"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 現在の設定をエクスポート
        private void エクスポートToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var sfd = new SaveFileDialog()) {
                sfd.Filter = "設定ファイル (*.json)|*.json";
                sfd.FileName = "rssSetting.json";
                if (sfd.ShowDialog() == DialogResult.OK) {
                    try {
                        _currentSettings.BackgroundColorHtml =
                            ColorTranslator.ToHtml(this.BackColor);
                        string json = JsonSerializer
                            .Serialize(_currentSettings
                            , new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(sfd.FileName, json);
                        MessageBox.Show("設定をエクスポートしました"
                            , "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"エクスポートに失敗しました\n{ex.Message}"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 背景色を変更する
        private void 背景色設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var cd = new ColorDialog()) {
                cd.Color = this.BackColor;
                if (cd.ShowDialog() == DialogResult.OK) {
                    this.BackColor = cd.Color;
                }
            }
        }

        // アプリケーションを終了
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close(); // FormClosingイベントで保存される
        }

        // 入力欄のテキストをクリア
        private void 入力欄をクリアToolStripMenuItem_Click(object sender, EventArgs e) {
            urlComboBox.SelectedIndex = -1;
            urlComboBox.Text = string.Empty;
            favoriteName.SelectedIndex = -1;
            favoriteName.Text = string.Empty;
        }

        // すべてのお気に入りを削除
        private void お気に入りを全て削除ToolStripMenuItem_Click(object sender, EventArgs e) {
            var result = MessageBox
                .Show("本当にお気に入りを全て削除してもよろしいですか？"
                , "最終確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                _currentSettings.FavoriteUrls.Clear();
                SaveSettings();
                UpdateComboBoxes();
                MessageBox.Show("すべてのお気に入りを削除しました"
                    , "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ヘルプ（取扱説明書）を表示する
        private void ヘルプToolStripMenuItem1_Click(object? sender, EventArgs e) {
            string helpText =
                "RSSリーダー アプリケーションの使い方\r\n" +
                "====================================\r\n" +
                "\r\n" +
                "■ 基本的な使い方\r\n" +
                "1. 画面上部のプルダウンメニューから読みたいニュースのカテゴリ（主要、国内など）を選択します。\r\n" +
                "   または、直接テキストボックスにRSSフィードのURLを入力することもできます。\r\n" +
                "2. 「データ取得」ボタンをクリックします。\r\n" +
                "3. 画面中段のリストに記事のタイトル一覧が表示されます。\r\n" +
                "4. タイトルをクリックすると、画面下部のブラウザ領域にWebページが表示されます。\r\n" +
                "5. ブラウザ領域の上にある「戻る」「進む」「再読み込み」ボタンで操作が可能です。\r\n" +
                "\r\n" +
                "■ お気に入り機能\r\n" +
                "【登録方法】\r\n" +
                "1. 上部のURL入力欄に、登録したいRSSフィードのURLを入力します。\r\n" +
                "2. 「お気に入り名称」入力欄に、分かりやすい名前を入力します。\r\n" +
                "3. 「登録」ボタンをクリックするとお気に入りに追加されます。\r\n" +
                "\r\n" +
                "【使い方】\r\n" +
                "・URL入力欄のプルダウンメニューから、登録したお気に入りの名前を選択して「データ取得」ボタンを押します。\r\n" +
                "\r\n" +
                "【削除方法】\r\n" +
                "1. 「お気に入り名称」のプルダウンメニューから、削除したいお気に入りの名前を選択します。\r\n" +
                "2. 「登録解除」ボタンをクリックします。\r\n" +
                "\r\n" +
                "■ メニューバーの機能\r\n" +
                "【ファイル】\r\n" +
                "・お気に入り設定 > インポート: 設定ファイル(rssSetting.json)を読み込み、お気に入りと背景色を復元します。\r\n" +
                "・お気に入り設定 > エクスポート: 現在のお気に入りと背景色の設定をファイルに保存します。\r\n" +
                "・背景色設定: アプリケーション全体の背景色を自由に変更できます。\r\n" +
                "・終了: アプリケーションを終了します。設定は自動的に保存されます。\r\n" +
                "\r\n" +
                "【編集】\r\n" +
                "・入力欄をクリア: URLとお気に入り名称の入力欄を空にします。\r\n" +
                "・お気に入りを全て削除: 登録したすべてのお気に入りを、確認ののちに削除します。\r\n" +
                "\r\n" +
                "【ヘルプ】\r\n" +
                "・ヘルプ: この使い方説明を表示します。\r\n" +
                "・バージョン情報: アプリケーションのバージョン情報を表示します。\r\n" +
                "\r\n" +
                "■ 設定の保存について\r\n" +
                "・お気に入り情報と背景色の設定は、アプリケーション終了時に実行ファイルと同じフォルダに`rssSetting.json`として自動的に保存され、次回起動時に読み込まれます。\r\n";

            using (var helpForm = new Form()) {
                helpForm.Text = "ヘルプ - 使い方";
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

        // バージョン情報を表示
        private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e) {
            string appName = "RSS Reader";
            string version = "1.025.7.25.3";
            MessageBox.Show($"{appName}\nバージョン: {version}"
                , "バージョン情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

    }
}