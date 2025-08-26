using System.Text.RegularExpressions;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
        }
        private static void Pickup3DigitNumber(string filePath) {
            foreach (var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line, @"\b[a-zA-Z]+\b");    
                foreach (Match m in matches) {
                    Console.WriteLine($"Index={m.Index}, Length={m.Length}, Value={m.Value}");
                }
            }
        }
            

    }
}
