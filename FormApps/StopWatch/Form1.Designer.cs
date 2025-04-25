namespace StopWatch {
    partial class mainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lbTimeDisp = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.tmDisp = new System.Windows.Forms.Timer(this.components);
            this.tbRestart = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tbRap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTimeDisp
            // 
            this.lbTimeDisp.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbTimeDisp.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTimeDisp.Location = new System.Drawing.Point(12, 9);
            this.lbTimeDisp.Name = "lbTimeDisp";
            this.lbTimeDisp.Size = new System.Drawing.Size(274, 61);
            this.lbTimeDisp.TabIndex = 0;
            this.lbTimeDisp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btStart
            // 
            this.btStart.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btStart.Location = new System.Drawing.Point(12, 90);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(128, 58);
            this.btStart.TabIndex = 1;
            this.btStart.Text = "スタート";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btReset
            // 
            this.btReset.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btReset.Location = new System.Drawing.Point(158, 90);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(128, 58);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "リセット";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btStop
            // 
            this.btStop.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btStop.Location = new System.Drawing.Point(12, 154);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(128, 58);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "ストップ";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // tmDisp
            // 
            this.tmDisp.Interval = 20;
            this.tmDisp.Tick += new System.EventHandler(this.tmDisp_Tick);
            // 
            // tbRestart
            // 
            this.tbRestart.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbRestart.Location = new System.Drawing.Point(158, 154);
            this.tbRestart.Name = "tbRestart";
            this.tbRestart.Size = new System.Drawing.Size(127, 58);
            this.tbRestart.TabIndex = 4;
            this.tbRestart.Text = "リスタート";
            this.tbRestart.UseVisualStyleBackColor = true;
            this.tbRestart.Click += new System.EventHandler(this.tbRestart_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(292, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(130, 136);
            this.listBox1.TabIndex = 0;
            this.listBox1.TabStop = false;
            // 
            // tbRap
            // 
            this.tbRap.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbRap.Location = new System.Drawing.Point(292, 154);
            this.tbRap.Name = "tbRap";
            this.tbRap.Size = new System.Drawing.Size(130, 58);
            this.tbRap.TabIndex = 5;
            this.tbRap.Text = "ラップ";
            this.tbRap.UseVisualStyleBackColor = true;
            this.tbRap.Click += new System.EventHandler(this.tbRap_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 226);
            this.Controls.Add(this.tbRap);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.tbRestart);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.lbTimeDisp);
            this.Name = "mainForm";
            this.Text = "TimerApp";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTimeDisp;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Timer tmDisp;
        private System.Windows.Forms.Button tbRestart;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button tbRap;
    }
}

