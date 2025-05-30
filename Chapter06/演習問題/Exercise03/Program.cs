
using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";
            #region 各演習問題の呼び出し
            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);
            #endregion

        }

        private static void Exercise1(string text) {
            Console.WriteLine("空白数：" + text.Count(c => char.IsWhiteSpace(c)));
        }

        private static void Exercise2(string text) {
            Console.WriteLine(text.Replace("big", "small"));
        }

        private static void Exercise3(string text) {
            //var StringBuilder = text.Split(' ');
            //Console.WriteLine(string.Join(" ", StringBuilder));
            //作成中...
        }

        private static void Exercise4(string text) {
            Console.WriteLine("単語数：" + text.Split(' ').ToArray().Length);
        }

        private static void Exercise5(string text) {
            Array.ForEach(text.Split(' ').Where(x => x.Length <= 4).ToArray(), word => Console.WriteLine(word));
        }

    }
}
