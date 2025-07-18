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
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            urlSelect = new Button();
            urlClear = new Button();
            rollback = new Button();
            forward = new Button();
            reflesh = new Button();
            favoriteAdd = new Button();
            favoriteLabel = new Label();
            favoriteRemove = new Button();
            urlComboBox = new ComboBox();
            favoriteName = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(620, 12);
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
            lbTitles.Location = new Point(12, 47);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(714, 124);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(12, 211);
            webView21.Name = "webView21";
            webView21.Size = new Size(933, 442);
            webView21.TabIndex = 3;
            webView21.ZoomFactor = 1D;
            // 
            // urlSelect
            // 
            urlSelect.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            urlSelect.Location = new Point(12, 11);
            urlSelect.Name = "urlSelect";
            urlSelect.Size = new Size(97, 29);
            urlSelect.TabIndex = 1;
            urlSelect.Text = "選択...";
            urlSelect.UseVisualStyleBackColor = true;
            urlSelect.Click += urlSelect_Click;
            // 
            // urlClear
            // 
            urlClear.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            urlClear.Location = new Point(115, 11);
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
            rollback.Location = new Point(12, 176);
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
            forward.Location = new Point(115, 177);
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
            reflesh.Location = new Point(218, 176);
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
            favoriteAdd.Location = new Point(683, 177);
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
            favoriteLabel.Location = new Point(321, 181);
            favoriteLabel.Name = "favoriteLabel";
            favoriteLabel.Size = new Size(118, 20);
            favoriteLabel.TabIndex = 5;
            favoriteLabel.Text = "お気に入り名称：";
            // 
            // favoriteRemove
            // 
            favoriteRemove.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteRemove.Location = new Point(786, 177);
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
            urlComboBox.Location = new Point(218, 11);
            urlComboBox.Name = "urlComboBox";
            urlComboBox.Size = new Size(396, 29);
            urlComboBox.TabIndex = 6;
            // 
            // favoriteName
            // 
            favoriteName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            favoriteName.FormattingEnabled = true;
            favoriteName.Location = new Point(445, 177);
            favoriteName.Name = "favoriteName";
            favoriteName.Size = new Size(232, 29);
            favoriteName.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(957, 665);
            Controls.Add(favoriteName);
            Controls.Add(urlComboBox);
            Controls.Add(favoriteLabel);
            Controls.Add(webView21);
            Controls.Add(lbTitles);
            Controls.Add(urlClear);
            Controls.Add(favoriteRemove);
            Controls.Add(favoriteAdd);
            Controls.Add(reflesh);
            Controls.Add(forward);
            Controls.Add(rollback);
            Controls.Add(urlSelect);
            Controls.Add(btRssGet);
            Name = "Form1";
            Text = "RSSリーダー";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Button urlSelect;
        private Button urlClear;
        private Button rollback;
        private Button forward;
        private Button reflesh;
        private Button favoriteAdd;
        private Label favoriteLabel;
        private Button favoriteRemove;
        private ComboBox urlComboBox;
        private ComboBox favoriteName;
    }
}
