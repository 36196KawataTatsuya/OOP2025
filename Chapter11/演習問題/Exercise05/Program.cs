using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Exercise05 {
    internal class Program {
        static void Main(string[] args) {
            var lines = File.ReadAllLines("sample.html");
            var sb = new StringBuilder();
            foreach (var line in lines) {
                var s = Regex.Replace(line,
                    @"<(/?)([A-Z][A-Z0-9]*)(.*)>",
                    m => {
                        return string.Format("<{0}{1}{2}>", m.Groups[1].Value,
                            m.Groups[2].Value.ToLower(),
                            m.Groups[3].Value);
                    }
                );
                sb.AppendLine(s);
            }
            File.WriteAllText("sampleOut.html", sb.ToString());

            var text = File.ReadAllText("sampleOut.html");
            Console.WriteLine(text);



        }
    }
}