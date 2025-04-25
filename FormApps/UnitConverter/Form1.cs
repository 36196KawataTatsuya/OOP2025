using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitConverter {
    public partial class form : Form {
        public form() {
            InitializeComponent();
        }

        int trackBarValue = 0;

        private void trackBar1_Scroll(object sender, EventArgs e) {
            trackBarValue = trackBar1.Value;
        }

        private void Exchange_Click(object sender, EventArgs e) {
            if (trackBarValue == 0) {
                if (tbNum1.Text == "") {
                    tbNum1.Text = 0.ToString();
                }
                int num1 = int.Parse(tbNum1.Text);
                double num2 = num1 * 0.3048;
                tbNum2.Text = num2.ToString();
            } else if (trackBarValue == 1) {
                if (tbNum1.Text == "") {
                    tbNum1.Text = 0.ToString();
                }
                int num1 = int.Parse(tbNum1.Text);
                double num2 = num1 * 0.3048;
                tbNum2.Text = num2.ToString();
            }
        }

        private void btCalc_Click(object sender, EventArgs e) {
            if (nudNum2.Value == 0) {
                
            } else if (9999999 < (nudNum1.Value / nudNum2.Value)) {
                nudAnswer.Value = 9999999;
                nudRest.Value = nudNum1.Value % nudNum2.Value;
            } else {
                nudRest.Value = 0;
                nudAnswer.Value = Math.Floor(nudNum1.Value / nudNum2.Value);
                nudRest.Value = nudNum1.Value % nudNum2.Value;
            }
        }
    }
}
