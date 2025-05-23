

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
        "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
    "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            Console.WriteLine("[ foreach ]");
            foreach (string l in langs) {
                if (l.Contains("S")) {
                    Console.WriteLine(l);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("[ for ]");
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains("S")) {
                    Console.WriteLine(langs[i]);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("[ while ]");
            int x = 0;
            while (x < langs.Count) {
                if (langs[x].Contains("S")) {
                    Console.WriteLine(langs[x]);
                }
                x++;
            }
            Console.WriteLine("");
        }

        private static void Exercise2(List<string> langs) {
            langs.Where(lang => lang.Contains("S")).ToList().ForEach(lang => Console.WriteLine(lang));
        }

        private static void Exercise3(List<string> langs) {
            var lang = langs.Find(L => L.Length == 10);
            if (lang == null) {
                lang = "unknown";
                Console.WriteLine(lang);
            } else {
                Console.WriteLine(lang);
            }
        }
    }
}
