using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("***** 文字列判定 *****");
            Console.Write("最初の文字列を入力してください: ");
            var str1 = RL();
            Console.Write("次の文字列を入力してください: ");
            var str2 = RL();
            if (String.Compare(str1, str2, ignoreCase:true) == 0) {
                Console.WriteLine("同じ文字列です。");
            } else {
                Console.WriteLine("異なる文字列です。");
            }


        }

        public static string? RL() {
            while (true) {
                var str = Console.ReadLine() ?? string.Empty;
                if (str == string.Empty) {
                    Console.WriteLine("空の文字列は入力できません。もう一度入力してください。");
                    Console.Write("文字列を入力してください: ");
                    continue;
                } else {
                    return str;
                }
            }
        }

    }
}
