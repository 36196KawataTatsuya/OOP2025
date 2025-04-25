namespace UnitConverter {
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
            label1 = new Label();
            tbBeforeConversion = new TextBox();
            tbAfterConvesion = new TextBox();
            ConvertButton = new Button();
            beforeLabel = new Label();
            afterLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(38, 9);
            label1.Name = "label1";
            label1.Size = new Size(218, 45);
            label1.TabIndex = 0;
            label1.Text = "距離換算アプリ";
            // 
            // tbBeforeConversion
            // 
            tbBeforeConversion.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbBeforeConversion.Location = new Point(141, 70);
            tbBeforeConversion.Name = "tbBeforeConversion";
            tbBeforeConversion.Size = new Size(124, 39);
            tbBeforeConversion.TabIndex = 1;
            // 
            // tbAfterConvesion
            // 
            tbAfterConvesion.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbAfterConvesion.Location = new Point(141, 211);
            tbAfterConvesion.Name = "tbAfterConvesion";
            tbAfterConvesion.ReadOnly = true;
            tbAfterConvesion.Size = new Size(124, 39);
            tbAfterConvesion.TabIndex = 1;
            // 
            // ConvertButton
            // 
            ConvertButton.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ConvertButton.Location = new Point(141, 139);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(124, 42);
            ConvertButton.TabIndex = 2;
            ConvertButton.Text = "変換";
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // beforeLabel
            // 
            beforeLabel.AutoSize = true;
            beforeLabel.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            beforeLabel.Location = new Point(52, 82);
            beforeLabel.Name = "beforeLabel";
            beforeLabel.Size = new Size(58, 21);
            beforeLabel.TabIndex = 3;
            beforeLabel.Text = "変換前";
            // 
            // afterLabel
            // 
            afterLabel.AutoSize = true;
            afterLabel.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            afterLabel.Location = new Point(52, 223);
            afterLabel.Name = "afterLabel";
            afterLabel.Size = new Size(58, 21);
            afterLabel.TabIndex = 3;
            afterLabel.Text = "変換後";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(311, 276);
            Controls.Add(afterLabel);
            Controls.Add(beforeLabel);
            Controls.Add(ConvertButton);
            Controls.Add(tbAfterConvesion);
            Controls.Add(tbBeforeConversion);
            Controls.Add(label1);
            Name = "Form1";
            Text = "距離変換アプリ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbBeforeConversion;
        private TextBox tbAfterConvesion;
        private Button ConvertButton;
        private Label beforeLabel;
        private Label afterLabel;
    }
}
