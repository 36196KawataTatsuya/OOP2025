namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }

        private static void Exercise1(string text) {
            /*var words = new Dictionary<char, int>();
            foreach (var c in text.ToUpper()) {
                if (char.IsLetter(c) && 'A' <= c && c <= 'Z') {
                    if (!words.ContainsKey(c)) {
                        words[c] = 1;
                    } else {
                        words[c]++;
                    }
                }
            }
            foreach (var writeLine in words.OrderBy(w => w.Key)) {
                if (writeLine.Value != 0) {
                    Console.WriteLine($"{writeLine.Key}: {writeLine.Value}");
                }
            }
            //words.ToList().ForEach(w => Console.WriteLine($"{w.Key}: {w.Value}"));
            */
            var words = new SortedDictionary<char, int>();
            foreach (var c in text.ToUpper()) {
                if (char.IsLetter(c) && 'A' <= c && c <= 'Z') {
                    if (!words.ContainsKey(c)) {
                        words[c] = 1;
                    } else {
                        words[c]++;
                    }
                }
            }
            foreach (var writeLine in words) {
                if (writeLine.Value != 0) {
                    Console.WriteLine($"{writeLine.Key}: {writeLine.Value}");
                }
            }
        }

        private static void Exercise2(string text) {

        }


    }
}
