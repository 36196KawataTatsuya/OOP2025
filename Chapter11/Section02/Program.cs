using System.Text.RegularExpressions;

namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            var text = "private List<string> result = new List<string>();";
            bool isMatch = Regex.IsMatch(text, @"\(\);$");

            if (isMatch) {
                Console.WriteLine(";で終わっています");
            } else {
                Console.WriteLine(";で終わっていません");
            }
        }
    }
}
