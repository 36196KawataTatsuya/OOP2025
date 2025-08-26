using System.Text.RegularExpressions;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            string[] texts = [
                "Time is money.",
                "What time is it?",
                "It will take time.",
                "We reorganized the timetable.",
            ];
            foreach (var line in texts) {
                var matches = Regex.Matches(line, @"\btime\b", RegexOptions.IgnoreCase);
                
                foreach (Match match2 in matches) {
                    Console.WriteLine($"{line}, {match2.Index}");
                }
            }

        }
    }
}
