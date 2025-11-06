namespace Section05 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("並列処理有");

            Parallel.For(0, 100, i => {
                Console.WriteLine($"処理中：{i + 1}");
                Thread.Sleep(100);
                Console.WriteLine($"処理完了：{i + 1}");
            });

            Console.WriteLine("並列処理無");

            for (int i = 0; i < 100; i++) {
                Console.WriteLine($"処理中：{i + 1}");
                Thread.Sleep(100);
                Console.WriteLine($"処理完了：{i + 1}");
            }

        }
    }
}
