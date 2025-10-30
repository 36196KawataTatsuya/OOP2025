using System.Diagnostics;

namespace Section03 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            toolStripStatusLabel1.Text = string.Empty;
            var elapsed = await DoLongTimeWorkAsync(4000);
            toolStripStatusLabel1.Text = $"{elapsed}ƒ~ƒŠ•b";
        }

        private async Task<long> DoLongTimeWorkAsync(int miliseconds) {
            var sw = Stopwatch.StartNew();
            await Task.Run(() => {
                System.Threading.Thread.Sleep(miliseconds);
            });
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

    }
}
