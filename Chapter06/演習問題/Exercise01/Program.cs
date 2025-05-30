using System.Globalization;
using System.Text.Json;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("***** 文字列判定 *****");
            Console.Write("設定言語を入力してください (en, jp): ");
            var lang = Lang();
            Console.Write("最初の文字列を入力してください: ");
            var str1 = RL();
            Console.Write("次の文字列を入力してください: ");
            var str2 = RL();

            if (lang.Equals("en")) {
                var cultureInfo = new CultureInfo("en-US");
                if (String.Compare(str1, str2,ignoreCase:true) == 0) {
                    Console.WriteLine("同じ文字列です。");
                } else {
                    Console.WriteLine("異なる文字列です。");
                }
            } else if (lang.Equals("ja")) {
                var cultureInfo = new CultureInfo("ja-JP");
                if (String.Compare(str1, str2, cultureInfo, CompareOptions.IgnoreKanaType) == 0) {
                    Console.WriteLine("同じ文字列です。");
                } else {
                    Console.WriteLine("異なる文字列です。");
                }
            }

        }

        public static string? RL() {
            while (true) {
                var str = Console.ReadLine() ?? string.Empty;
                if (String.IsNullOrEmpty(str)) {
                    Console.WriteLine("空の文字列は入力できません。もう一度入力してください。");
                    Console.Write("文字列を入力してください: ");
                    continue;
                } else {
                    return str;
                }
            }
        }

        public static string Lang() {
            while (true) {
                var lang = Console.ReadLine() ?? string.Empty;
                if (String.IsNullOrEmpty(lang)) {
                    Console.WriteLine("空の文字列は入力できません。もう一度入力してください。");
                    Console.Write("設定言語を入力してください: ");
                    continue;
                } else if (lang != "en" && lang != "jp") {
                    Console.WriteLine("無効な言語コードです。'en' または 'jp' を入力してください。");
                    Console.Write("設定言語を入力してください (en, jp): ");
                    continue;
                } else {
                    return lang;
                }
            }
        }

    }
}
