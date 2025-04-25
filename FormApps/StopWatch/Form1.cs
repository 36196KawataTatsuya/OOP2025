using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace StopWatch {
    public partial class mainForm : Form {
        public mainForm() {
            InitializeComponent();
        }

        Stopwatch sw = new Stopwatch();

        private void mainForm_Load(object sender, EventArgs e) {
            lbTimeDisp.Text = "00:00:00.00";
            tmDisp.Interval = 10;
            
        }

        //スタートボタンのイベントハンドラー
        private void btStart_Click(object sender, EventArgs e) {
            sw.Start();
            tmDisp.Start();
        }

        private void tmDisp_Tick(object sender, EventArgs e) {
            lbTimeDisp.Text = sw.Elapsed.ToString(@"hh\:mm\:ss\.ff");
        }

        private void btStop_Click(object sender, EventArgs e) {
            sw.Stop();
            tmDisp.Stop();

        }

        private void btReset_Click(object sender, EventArgs e) {
            sw.Reset();
        }

        private void tbRestart_Click(object sender, EventArgs e) {
            sw.Restart();
        }

        private void tbRap_Click(object sender, EventArgs e) {
           listBox1.Items.Insert(0, lbTimeDisp.Text);
        }
    }
}
