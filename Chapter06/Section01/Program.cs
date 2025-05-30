using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var str1 = "APPLE";
            var str2 = "apple";
            var cultureInfo = new CultureInfo("ja-JP");
            if (String.Compare(str1, str2, cultureInfo, CompareOptions.IgnoreKanaType) == 0) {
                Console.WriteLine("同じ文字列です。");
            } else {
                Console.WriteLine("異なる文字列です。");
            }

        }
    }
}
