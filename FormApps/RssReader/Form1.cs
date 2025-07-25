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
        //お気に入り登録を保存する辞書の事前作成
        private Dictionary<string, string> _favoriteUrlDictionary = new Dictionary<string, string>();
        //設定ファイル名 (確定版)
        private const string SettingsFileName = "rssSetting.json";

        public Form1() {
            InitializeComponent();
            InitializeComboBox();
            rollback.Enabled = false;
            forward.Enabled = false;
        }

        //コンボボックス類を初期化する
        private void InitializeComboBox() {
            //お気に入り読み込み
            LoadFavorites();
            //ComboBoxの初期化
            UpdateComboBoxes();
            //URLコンボボックスを非選択状態にする
            urlComboBox.SelectedIndex = -1;
        }

        //設定ファイルからお気に入りを読み込む
        private void LoadFavorites() {
            //ファイルがあれば読み込む
            if (File.Exists(SettingsFileName)) {
                //JSONファイルを読んで辞書に変換
                try {
                    string json = File.ReadAllText(SettingsFileName);
                    _favoriteUrlDictionary = JsonSerializer
                        .Deserialize<Dictionary<string, string>>(json)
                        ?? new Dictionary<string, string>();
                }
                //失敗時にエラーメッセージを表示
                catch (Exception e) {
                    MessageBox
                        .Show($"設定ファイルが読み込まれませんでした\n{e.Message}"
                        , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _favoriteUrlDictionary = new Dictionary<string, string>();
                }
            }
        }

        //コンボボックスの更新
        private void UpdateComboBoxes() {
            //urlComboBoxの更新
            urlComboBox.DataSource = null;

            //デフォルトのURL辞書とお気に入りURL辞書を結合し、リストにする
            var binedList = _defaultUrlDictionary
                .Concat(_favoriteUrlDictionary).ToList();
            //上記で結合したリストをComboBoxにバインド
            urlComboBox.DataSource = binedList;
            urlComboBox.DisplayMember = "Key";
            urlComboBox.ValueMember = "Value";

            // favoriteNameコンボボックスの更新
            favoriteName.DataSource = null;

            //favoriteNameに名前を表示するためのリストを作成
            var favoriteList = _favoriteUrlDictionary.ToList();
            favoriteName.DataSource = favoriteList;

            //データが1つ以上登録されている時のみ、DisplayMemberとValueMemberを設定
            if (favoriteList.Count > 0) {
                favoriteName.DisplayMember = "Key";
                favoriteName.ValueMember = "Value";
            }

            //全ての選択状態をクリア
            urlComboBox.SelectedIndex = -1;
            urlComboBox.Text = string.Empty;
            favoriteName.SelectedIndex = -1;
            favoriteName.Text = string.Empty;
        }

        //RSSを取得する時のイベントハンドラ
        private async void btRssGet_Click(object sender, EventArgs e) {
            //URLコンボボックスから選択したもののURLを取得
            string? url = urlComboBox.SelectedValue as string ?? urlComboBox.Text;
            //通常入力の場合の例外処理を実施
            if (string.IsNullOrWhiteSpace(urlComboBox.Text)) {
                MessageBox.Show("URLが正しく入力されていません\n" +
                    "テキストボックスを確認してください", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? result)) {
                MessageBox.Show("入力された文字列は有効なURLではありません\n" +
                    "正しいURLの形式で入力してください\n" + url, "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var hc = new HttpClient()) {

                //xmlを取得 (HTTPクライアント作成)
                string xml = await hc.GetStringAsync(url);
                XDocument xdoc = XDocument.Parse(xml); //RSSの取得

                //アイテムデータを取得
                items = xdoc.Root!.Descendants("item")
                    .Select(x => new ItemData {
                        Title = (string?)x.Element("title") ?? "No Title",
                        Link = (string?)x.Element("link") ?? "No Link"
                    }).ToList();
            }
            //webビューアを初期化
            webView2Init = true;
            lbTitles.Items.Clear();
            lbTitles.Items.AddRange(items.ToArray());
            lbTitles.DisplayMember = "Title";

        }

        //リストボックスからWebView2にURLを渡す
        private void lbTitles_Click(object sender, EventArgs e) {
            //リストボックスの選択したアイテムからリンクを取得
            string? linkUrl = null;
            if (lbTitles.SelectedItem is ItemData selectedItem) {
                linkUrl = selectedItem.Link;
            }
            //正常にリンクが取得できた場合はWebView2に表示
            if (!string.IsNullOrWhiteSpace(linkUrl) && Uri
                .TryCreate(linkUrl, UriKind.Absolute, out Uri? uriResult)) {
                wvRssLink.Source = uriResult;
            }
        }

        //お気に入り登録の処理
        private void favoriteAdd_Click(object sender, EventArgs e) {
            string url = urlComboBox.Text;
            string name = favoriteName.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url)) {
                MessageBox.Show("お気に入り名称とURLの両方を入力してください"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out _)) {
                MessageBox.Show("有効なURLではありません", "エラー"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_defaultUrlDictionary.ContainsKey(name)
                || _favoriteUrlDictionary.ContainsKey(name)) {
                MessageBox.Show("その名称は既に使用されています", "エラー"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // お気に入りに追加
            _favoriteUrlDictionary.Add(name, url);
            SaveFavorites();
            UpdateComboBoxes();

            MessageBox.Show($"「{name}」をお気に入りに追加しました。"
                , "追加完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // お気に入りをファイルに保存する
        private void SaveFavorites() {
            //設定ファイルに保存する
            try {
                string json = JsonSerializer
                    .Serialize(_favoriteUrlDictionary
                    , new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFileName, json);
            }
            catch (Exception e) {
                MessageBox.Show($"設定ファイルの保存に失敗しました\n{e.Message}"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //お気に入り削除の処理
        private void favoriteRemove_Click(object sender, EventArgs e) {
            //選択できているかをチェック
            if (favoriteName.SelectedItem == null) {
                MessageBox.Show("削除するお気に入りを一覧から選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //選択されたアイテムのキーを取得
            string selectedKey =
                ((KeyValuePair<string, string>)favoriteName.SelectedItem).Key;

            //選択アイテムの確認ダイアログを表示
            var result = MessageBox.Show($"「{selectedKey}」を削除しますか？"
                , "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                //削除を実行
                _favoriteUrlDictionary.Remove(selectedKey);
                //変更を保存して更新
                SaveFavorites();
                UpdateComboBoxes();

                MessageBox.Show("削除しました"
                    , "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //進む・戻るボタンのイベントハンドラ
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

        //urlComboBoxの入力内容クリア
        private void urlClear_Click(object sender, EventArgs e) {
            urlComboBox.Text = string.Empty;
        }

        //以下、WebView2の詳細なイベントハンドラ
        private void rollback_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }
        private void forward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }
        private void reflesh_Click(object sender, EventArgs e) {
            if (webView2Init == false) {
                MessageBox.Show
                    ("RSSを取得してからリフレッシュしてください", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                wvRssLink.Reload();
            }
        }

    }
}
