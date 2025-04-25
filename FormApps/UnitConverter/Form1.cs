using System.Windows.Forms.VisualStyles;

namespace UnitConverter {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, EventArgs e) {
            while (true) {
                if (int.TryParse(tbBeforeConversion.Text, out int output)) {
                    double outputValue = output * 0.0254;
                }
            }
            int inputValue = int.Parse(tbBeforeConversion.Text);

            double outputValue = inputValue * 0.0254;

            tbAfterConvesion.Text = outputValue.ToString();
        }
    }
}
