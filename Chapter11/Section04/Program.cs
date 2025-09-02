using System.Text.RegularExpressions;

namespace Section04 {
    internal class Program {
        static void Main(string[] args) {

            var lines = File.ReadAllLines("sample.txt");
            //問題11,4
            var newLines = lines.Select(l => Regex.Replace(l, @"\bversion\s*=\s*""v4.0""", @"version=""v5.0""")).ToArray();

            File.WriteAllLines("sampleChange.txt", newLines);

            //確認用
            var text = File.ReadAllText("sampleChange.txt");
            Console.WriteLine(text);

        }
    }
}
