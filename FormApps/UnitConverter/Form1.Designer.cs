namespace UnitConverter {
    partial class form {
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
            this.mainLabel = new System.Windows.Forms.Label();
            this.tbNum1 = new System.Windows.Forms.TextBox();
            this.beforeExcangeLabel = new System.Windows.Forms.Label();
            this.afterExcangeLabel = new System.Windows.Forms.Label();
            this.tbNum2 = new System.Windows.Forms.TextBox();
            this.Exchange = new System.Windows.Forms.Button();
            this.arrow1 = new System.Windows.Forms.Label();
            this.arrow2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.feetLabel = new System.Windows.Forms.Label();
            this.MeterLabel = new System.Windows.Forms.Label();
            this.nudNum1 = new System.Windows.Forms.NumericUpDown();
            this.cut = new System.Windows.Forms.Label();
            this.nudNum2 = new System.Windows.Forms.NumericUpDown();
            this.equal = new System.Windows.Forms.Label();
            this.btCalc = new System.Windows.Forms.Button();
            this.nudAnswer = new System.Windows.Forms.NumericUpDown();
            this.nudRest = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnswer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRest)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.BackColor = System.Drawing.Color.DarkGray;
            this.mainLabel.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainLabel.Location = new System.Drawing.Point(12, 9);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(312, 48);
            this.mainLabel.TabIndex = 0;
            this.mainLabel.Text = "距離換算アプリ";
            // 
            // tbNum1
            // 
            this.tbNum1.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbNum1.Location = new System.Drawing.Point(114, 83);
            this.tbNum1.Name = "tbNum1";
            this.tbNum1.Size = new System.Drawing.Size(210, 42);
            this.tbNum1.TabIndex = 1;
            // 
            // beforeExcangeLabel
            // 
            this.beforeExcangeLabel.AutoSize = true;
            this.beforeExcangeLabel.BackColor = System.Drawing.Color.DarkGray;
            this.beforeExcangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.beforeExcangeLabel.Location = new System.Drawing.Point(16, 98);
            this.beforeExcangeLabel.Name = "beforeExcangeLabel";
            this.beforeExcangeLabel.Size = new System.Drawing.Size(73, 21);
            this.beforeExcangeLabel.TabIndex = 2;
            this.beforeExcangeLabel.Text = "変換前";
            // 
            // afterExcangeLabel
            // 
            this.afterExcangeLabel.AutoSize = true;
            this.afterExcangeLabel.BackColor = System.Drawing.Color.DarkGray;
            this.afterExcangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.afterExcangeLabel.Location = new System.Drawing.Point(16, 274);
            this.afterExcangeLabel.Name = "afterExcangeLabel";
            this.afterExcangeLabel.Size = new System.Drawing.Size(73, 21);
            this.afterExcangeLabel.TabIndex = 2;
            this.afterExcangeLabel.Text = "変換後";
            // 
            // tbNum2
            // 
            this.tbNum2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbNum2.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbNum2.Location = new System.Drawing.Point(114, 259);
            this.tbNum2.Name = "tbNum2";
            this.tbNum2.ReadOnly = true;
            this.tbNum2.Size = new System.Drawing.Size(210, 42);
            this.tbNum2.TabIndex = 1;
            // 
            // Exchange
            // 
            this.Exchange.BackColor = System.Drawing.Color.White;
            this.Exchange.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Exchange.Location = new System.Drawing.Point(164, 170);
            this.Exchange.Name = "Exchange";
            this.Exchange.Size = new System.Drawing.Size(105, 47);
            this.Exchange.TabIndex = 3;
            this.Exchange.Text = "変換";
            this.Exchange.UseVisualStyleBackColor = false;
            this.Exchange.Click += new System.EventHandler(this.Exchange_Click);
            // 
            // arrow1
            // 
            this.arrow1.AutoSize = true;
            this.arrow1.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.arrow1.Location = new System.Drawing.Point(193, 132);
            this.arrow1.Name = "arrow1";
            this.arrow1.Size = new System.Drawing.Size(50, 35);
            this.arrow1.TabIndex = 4;
            this.arrow1.Text = "↓";
            // 
            // arrow2
            // 
            this.arrow2.AutoSize = true;
            this.arrow2.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.arrow2.Location = new System.Drawing.Point(193, 221);
            this.arrow2.Name = "arrow2";
            this.arrow2.Size = new System.Drawing.Size(50, 35);
            this.arrow2.TabIndex = 4;
            this.arrow2.Text = "↓";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.Color.DarkGray;
            this.trackBar1.Location = new System.Drawing.Point(12, 198);
            this.trackBar1.Maximum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // feetLabel
            // 
            this.feetLabel.AutoSize = true;
            this.feetLabel.BackColor = System.Drawing.Color.DarkGray;
            this.feetLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.feetLabel.Location = new System.Drawing.Point(8, 170);
            this.feetLabel.Name = "feetLabel";
            this.feetLabel.Size = new System.Drawing.Size(55, 21);
            this.feetLabel.TabIndex = 6;
            this.feetLabel.Text = "Feet";
            // 
            // MeterLabel
            // 
            this.MeterLabel.AutoSize = true;
            this.MeterLabel.BackColor = System.Drawing.Color.DarkGray;
            this.MeterLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MeterLabel.Location = new System.Drawing.Point(69, 170);
            this.MeterLabel.Name = "MeterLabel";
            this.MeterLabel.Size = new System.Drawing.Size(67, 21);
            this.MeterLabel.TabIndex = 6;
            this.MeterLabel.Text = "Meter";
            // 
            // nudNum1
            // 
            this.nudNum1.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudNum1.Location = new System.Drawing.Point(18, 329);
            this.nudNum1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudNum1.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.nudNum1.Name = "nudNum1";
            this.nudNum1.Size = new System.Drawing.Size(133, 36);
            this.nudNum1.TabIndex = 7;
            // 
            // cut
            // 
            this.cut.AutoSize = true;
            this.cut.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cut.Location = new System.Drawing.Point(159, 331);
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(43, 29);
            this.cut.TabIndex = 8;
            this.cut.Text = "÷";
            // 
            // nudNum2
            // 
            this.nudNum2.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudNum2.Location = new System.Drawing.Point(204, 329);
            this.nudNum2.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudNum2.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.nudNum2.Name = "nudNum2";
            this.nudNum2.Size = new System.Drawing.Size(133, 36);
            this.nudNum2.TabIndex = 7;
            // 
            // equal
            // 
            this.equal.AutoSize = true;
            this.equal.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.equal.Location = new System.Drawing.Point(344, 333);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(43, 29);
            this.equal.TabIndex = 8;
            this.equal.Text = "＝";
            // 
            // btCalc
            // 
            this.btCalc.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btCalc.Location = new System.Drawing.Point(387, 393);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(133, 50);
            this.btCalc.TabIndex = 9;
            this.btCalc.Text = "計算";
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.btCalc_Click);
            // 
            // nudAnswer
            // 
            this.nudAnswer.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudAnswer.Location = new System.Drawing.Point(387, 329);
            this.nudAnswer.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudAnswer.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.nudAnswer.Name = "nudAnswer";
            this.nudAnswer.Size = new System.Drawing.Size(133, 36);
            this.nudAnswer.TabIndex = 7;
            // 
            // nudRest
            // 
            this.nudRest.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudRest.Location = new System.Drawing.Point(643, 329);
            this.nudRest.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudRest.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.nudRest.Name = "nudRest";
            this.nudRest.Size = new System.Drawing.Size(133, 36);
            this.nudRest.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(542, 331);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "あまり";
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(806, 486);
            this.Controls.Add(this.btCalc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.equal);
            this.Controls.Add(this.cut);
            this.Controls.Add(this.nudRest);
            this.Controls.Add(this.nudAnswer);
            this.Controls.Add(this.nudNum2);
            this.Controls.Add(this.nudNum1);
            this.Controls.Add(this.MeterLabel);
            this.Controls.Add(this.feetLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.arrow2);
            this.Controls.Add(this.arrow1);
            this.Controls.Add(this.Exchange);
            this.Controls.Add(this.afterExcangeLabel);
            this.Controls.Add(this.beforeExcangeLabel);
            this.Controls.Add(this.tbNum2);
            this.Controls.Add(this.tbNum1);
            this.Controls.Add(this.mainLabel);
            this.Name = "form";
            this.Text = "距離換算アプリ";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnswer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.TextBox tbNum1;
        private System.Windows.Forms.Label beforeExcangeLabel;
        private System.Windows.Forms.Label afterExcangeLabel;
        private System.Windows.Forms.TextBox tbNum2;
        private System.Windows.Forms.Button Exchange;
        private System.Windows.Forms.Label arrow1;
        private System.Windows.Forms.Label arrow2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label feetLabel;
        private System.Windows.Forms.Label MeterLabel;
        private System.Windows.Forms.NumericUpDown nudNum1;
        private System.Windows.Forms.Label cut;
        private System.Windows.Forms.NumericUpDown nudNum2;
        private System.Windows.Forms.Label equal;
        private System.Windows.Forms.Button btCalc;
        private System.Windows.Forms.NumericUpDown nudAnswer;
        private System.Windows.Forms.NumericUpDown nudRest;
        private System.Windows.Forms.Label label1;
    }
}

