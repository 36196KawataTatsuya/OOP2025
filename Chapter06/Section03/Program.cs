using System.Text;

namespace Section03 {
    internal class Program {
        static void Main(string[] args) {
            //StringBuilerを利用
            var sb = new StringBuilder();
            foreach (var word in GetWords()) {
                sb.Append(word);
            }
            Console.WriteLine(sb);

            //文字列の連携
            String str = "";
            foreach (var word in GetWords()) {
                str += word;
            }
            Console.WriteLine(str);
        }

        private static IEnumerable<object> GetWords() {
            return ["Orange", "Lemon", "Strawberry"];
        }


    }
}
