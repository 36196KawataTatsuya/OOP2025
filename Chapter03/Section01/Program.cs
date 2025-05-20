
using System.Linq;
using System.Xml.Linq;

namespace Section01 {
    internal class Program {

        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London",
                "Paris", "Berlin", "Canberra", "Hong Kong",
            };

            Console.WriteLine("***** 3.2.1 *****");
            Exercise2_1(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.2 *****");
            Exercise2_2(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.3 *****");
            Exercise2_3(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.4 *****");
            Exercise2_4(cities);
            Console.WriteLine();


            
        }

        private static void Exercise2_1(List<string> names) {
            while (true) {
                Console.WriteLine("都市名を入力 / 空行で終了");
                var name = Console.ReadLine();
                int index = names.FindIndex(s => s.Equals(name));
                if (string.IsNullOrWhiteSpace(name)) {
                    break;
                } else if (index == -1) {
                    Console.WriteLine("見つかりませんでした\nもう一度入力してください\n");
                } else {
                    Console.WriteLine($"インデックスが見つかりました：{index}");
                }
            }
        }

        private static void Exercise2_2(List<string> names) {
            Console.WriteLine(names.Count(n => n.ToString().Contains('o')));
        }

        private static void Exercise2_3(List<string> names) {
            var selected = names.Where(w => w.ToString().Contains('o')).ToArray();
            foreach (var output in selected) {
                Console.WriteLine(output);
            }
        }

        private static void Exercise2_4(List<string> names) {
            var counter = names
                .Where(w => w
                .ToString()
                .StartsWith('B'))
                .Select(s => new { s, s.Length });

            foreach (var name in counter) {
                Console.WriteLine(name.s + ": " + name.Length + "文字" );
            }
        }

    }
}