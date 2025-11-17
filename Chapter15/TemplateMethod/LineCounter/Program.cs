using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("ファイルパスを入力：");
            while (true) {
                string? input = Console.ReadLine();
                string result = input.Replace("\"", "");
                if (!File.Exists(result)) {
                    Console.WriteLine("指定されたファイルパスが存在しないか、無効です。再度入力してください。");
                    Console.Write("ファイルパスを入力：");
                } else if (string.IsNullOrEmpty(result)) {
                    Console.WriteLine("ファイルパスが不正です。再度入力してください。");
                    Console.Write("ファイルパスを入力：");
                } else {
                    TextProcessor.Run<LineCounterProcessor>(result);
                    break;
                }
            }
        }
    }
}
