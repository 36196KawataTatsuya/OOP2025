namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            urlClear = new Button();
            rollback = new Button();
            forward = new Button();
            reflesh = new Button();
            favoriteAdd = new Button();
            favoriteLabel = new Label();
            favoriteRemove = new Button();
            urlComboBox = new ComboBox();
            favoriteName = new ComboBox();
            menuStrip1 = new MenuStrip();
            ファイルToolStripMenuItem = new ToolStripMenuItem();
            お気に入り設定ToolStripMenuItem = new ToolStripMenuItem();
            インポートToolStripMenuItem = new ToolStripMenuItem();
            エクスポートToolStripMenuItem = new ToolStripMenuItem();
            背景色設定ToolStripMenuItem = new ToolStripMenuItem();
            終了ToolStripMenuItem = new ToolStripMenuItem();
            編集ToolStripMenuItem = new ToolStripMenuItem();
            入力欄をクリアToolStripMenuItem = new ToolStripMenuItem();
            お気に入りを全て削除ToolStripMenuItem = new ToolStripMenuItem();
            ヘルプToolStripMenuItem = new ToolStripMenuItem();
            ヘルプToolStripMenuItem1 = new ToolStripMenuItem();
            バージョン情報ToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(674, 27);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(106, 29);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "データ取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 15;
            lbTitles.Location = new Point(12, 62);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(714, 124);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.BackColor = SystemColors.ControlDark;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(12, 227);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(1040, 426);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.SourceChanged += wvRssLink_SourceChanged;
            // 
            // urlClear
            // 
            urlClear.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            urlClear.Location = new Point(12, 27);
            urlClear.Name = "urlClear";
            urlClear.Size = new Size(97, 29);
            urlClear.TabIndex = 1;
            urlClear.Text = "クリア";
            urlClear.UseVisualStyleBackColor = true;
            urlClear.Click += urlClear_Click;
            // 
            // rollback
            // 
            rollback.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            rollback.Location = new Point(12, 191);
            rollback.Name = "rollback";
            rollback.Size = new Size(97, 29);
            rollback.TabIndex = 1;
            rollback.Text = "戻る";
            rollback.UseVisualStyleBackColor = true;
            rollback.Click += rollback_Click;
            // 
            // forward
            // 
            forward.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            forward.Location = new Point(115, 192);
            forward.Name = "forward";
            forward.Size = new Size(97, 29);
            forward.TabIndex = 1;
            forward.Text = "進む";
            forward.UseVisualStyleBackColor = true;
            forward.Click += forward_Click;
            // 
            // reflesh
            // 
            reflesh.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            reflesh.Location = new Point(218, 191);
            reflesh.Name = "reflesh";
            reflesh.Size = new Size(97, 29);
            reflesh.TabIndex = 1;
            reflesh.Text = "再読み込み";
            reflesh.UseVisualStyleBackColor = true;
            reflesh.Click += reflesh_Click;
            // 
            // favoriteAdd
            // 
            favoriteAdd.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteAdd.Location = new Point(683, 192);
            favoriteAdd.Name = "favoriteAdd";
            favoriteAdd.Size = new Size(97, 29);
            favoriteAdd.TabIndex = 1;
            favoriteAdd.Text = "登録";
            favoriteAdd.UseVisualStyleBackColor = true;
            favoriteAdd.Click += favoriteAdd_Click;
            // 
            // favoriteLabel
            // 
            favoriteLabel.AutoSize = true;
            favoriteLabel.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteLabel.Location = new Point(321, 196);
            favoriteLabel.Name = "favoriteLabel";
            favoriteLabel.Size = new Size(118, 20);
            favoriteLabel.TabIndex = 5;
            favoriteLabel.Text = "お気に入り名称：";
            // 
            // favoriteRemove
            // 
            favoriteRemove.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteRemove.Location = new Point(786, 192);
            favoriteRemove.Name = "favoriteRemove";
            favoriteRemove.Size = new Size(97, 29);
            favoriteRemove.TabIndex = 1;
            favoriteRemove.Text = "登録解除";
            favoriteRemove.UseVisualStyleBackColor = true;
            favoriteRemove.Click += favoriteRemove_Click;
            // 
            // urlComboBox
            // 
            urlComboBox.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            urlComboBox.FormattingEnabled = true;
            urlComboBox.Location = new Point(115, 27);
            urlComboBox.Name = "urlComboBox";
            urlComboBox.Size = new Size(553, 29);
            urlComboBox.TabIndex = 6;
            // 
            // favoriteName
            // 
            favoriteName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteName.FormattingEnabled = true;
            favoriteName.Location = new Point(445, 192);
            favoriteName.Name = "favoriteName";
            favoriteName.Size = new Size(232, 29);
            favoriteName.TabIndex = 6;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルToolStripMenuItem, 編集ToolStripMenuItem, ヘルプToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1064, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            ファイルToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { お気に入り設定ToolStripMenuItem, 背景色設定ToolStripMenuItem, 終了ToolStripMenuItem });
            ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            ファイルToolStripMenuItem.Size = new Size(53, 20);
            ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // お気に入り設定ToolStripMenuItem
            // 
            お気に入り設定ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { インポートToolStripMenuItem, エクスポートToolStripMenuItem });
            お気に入り設定ToolStripMenuItem.Name = "お気に入り設定ToolStripMenuItem";
            お気に入り設定ToolStripMenuItem.Size = new Size(149, 22);
            お気に入り設定ToolStripMenuItem.Text = "お気に入り設定";
            // 
            // インポートToolStripMenuItem
            // 
            インポートToolStripMenuItem.Name = "インポートToolStripMenuItem";
            インポートToolStripMenuItem.Size = new Size(133, 22);
            インポートToolStripMenuItem.Text = "インポート..";
            インポートToolStripMenuItem.Click += インポートToolStripMenuItem_Click;
            // 
            // エクスポートToolStripMenuItem
            // 
            エクスポートToolStripMenuItem.Name = "エクスポートToolStripMenuItem";
            エクスポートToolStripMenuItem.Size = new Size(133, 22);
            エクスポートToolStripMenuItem.Text = "エクスポート..";
            エクスポートToolStripMenuItem.Click += エクスポートToolStripMenuItem_Click;
            // 
            // 背景色設定ToolStripMenuItem
            // 
            背景色設定ToolStripMenuItem.Name = "背景色設定ToolStripMenuItem";
            背景色設定ToolStripMenuItem.Size = new Size(149, 22);
            背景色設定ToolStripMenuItem.Text = "背景色設定";
            背景色設定ToolStripMenuItem.Click += 背景色設定ToolStripMenuItem_Click;
            // 
            // 終了ToolStripMenuItem
            // 
            終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            終了ToolStripMenuItem.Size = new Size(149, 22);
            終了ToolStripMenuItem.Text = "終了";
            終了ToolStripMenuItem.Click += 終了ToolStripMenuItem_Click;
            // 
            // 編集ToolStripMenuItem
            // 
            編集ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 入力欄をクリアToolStripMenuItem, お気に入りを全て削除ToolStripMenuItem });
            編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
            編集ToolStripMenuItem.Size = new Size(43, 20);
            編集ToolStripMenuItem.Text = "編集";
            // 
            // 入力欄をクリアToolStripMenuItem
            // 
            入力欄をクリアToolStripMenuItem.Name = "入力欄をクリアToolStripMenuItem";
            入力欄をクリアToolStripMenuItem.Size = new Size(179, 22);
            入力欄をクリアToolStripMenuItem.Text = "入力欄をクリア";
            入力欄をクリアToolStripMenuItem.Click += 入力欄をクリアToolStripMenuItem_Click;
            // 
            // お気に入りを全て削除ToolStripMenuItem
            // 
            お気に入りを全て削除ToolStripMenuItem.Name = "お気に入りを全て削除ToolStripMenuItem";
            お気に入りを全て削除ToolStripMenuItem.Size = new Size(179, 22);
            お気に入りを全て削除ToolStripMenuItem.Text = "お気に入りを全て削除";
            お気に入りを全て削除ToolStripMenuItem.Click += お気に入りを全て削除ToolStripMenuItem_Click;
            // 
            // ヘルプToolStripMenuItem
            // 
            ヘルプToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ヘルプToolStripMenuItem1, バージョン情報ToolStripMenuItem });
            ヘルプToolStripMenuItem.Name = "ヘルプToolStripMenuItem";
            ヘルプToolStripMenuItem.Size = new Size(48, 20);
            ヘルプToolStripMenuItem.Text = "ヘルプ";
            // 
            // ヘルプToolStripMenuItem1
            // 
            ヘルプToolStripMenuItem1.Name = "ヘルプToolStripMenuItem1";
            ヘルプToolStripMenuItem1.Size = new Size(142, 22);
            ヘルプToolStripMenuItem1.Text = "ヘルプ";
            ヘルプToolStripMenuItem1.Click += ヘルプToolStripMenuItem1_Click;
            // 
            // バージョン情報ToolStripMenuItem
            // 
            バージョン情報ToolStripMenuItem.Name = "バージョン情報ToolStripMenuItem";
            バージョン情報ToolStripMenuItem.Size = new Size(142, 22);
            バージョン情報ToolStripMenuItem.Text = "バージョン情報";
            バージョン情報ToolStripMenuItem.Click += バージョン情報ToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 665);
            Controls.Add(favoriteName);
            Controls.Add(urlComboBox);
            Controls.Add(favoriteLabel);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(urlClear);
            Controls.Add(favoriteRemove);
            Controls.Add(favoriteAdd);
            Controls.Add(reflesh);
            Controls.Add(forward);
            Controls.Add(rollback);
            Controls.Add(btRssGet);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "RSSリーダー";
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button urlClear;
        private Button rollback;
        private Button forward;
        private Button reflesh;
        private Button favoriteAdd;
        private Label favoriteLabel;
        private Button favoriteRemove;
        private ComboBox urlComboBox;
        private ComboBox favoriteName;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルToolStripMenuItem;
        private ToolStripMenuItem お気に入り設定ToolStripMenuItem;
        private ToolStripMenuItem インポートToolStripMenuItem;
        private ToolStripMenuItem エクスポートToolStripMenuItem;
        private ToolStripMenuItem 背景色設定ToolStripMenuItem;
        private ToolStripMenuItem 終了ToolStripMenuItem;
        private ToolStripMenuItem 編集ToolStripMenuItem;
        private ToolStripMenuItem 入力欄をクリアToolStripMenuItem;
        private ToolStripMenuItem お気に入りを全て削除ToolStripMenuItem;
        private ToolStripMenuItem ヘルプToolStripMenuItem;
        private ToolStripMenuItem ヘルプToolStripMenuItem1;
        private ToolStripMenuItem バージョン情報ToolStripMenuItem;
    }
}
