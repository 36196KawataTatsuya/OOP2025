using System.Text.RegularExpressions;

namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            var strings = new[] {
                "Microsoft Windows",
                "windows",
                "windows Server",
                "Windows",
            };
            var regex = new Regex(@"^(W|w)indows$");
            var count = strings.Count(s => regex.IsMatch(s));
            Console.WriteLine($"{count}行と一致");

            //パターンと完全一致している文字列を出力
            foreach (var s in strings) {
                if (regex.IsMatch(s)) {
                    Console.WriteLine($"{s}");
                }
            }
            //LINQを使った場合
            //strings.ToList().ForEach(s => {if(regex.IsMatch(s)){Console.WriteLine($"{s}");}});
        }
    }
}
