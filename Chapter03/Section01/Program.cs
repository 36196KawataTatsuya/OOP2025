namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(Count(ParseNum())); //入力した数値を配列から検索して個数を出力

        }

        static int ParseNum() {
            int countNum = 0;
            while (true) {
                Console.Write("カウントしたい数値：");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out countNum)) {
                    break; // 正常に入力されたらループ終了    
                } else { // 変換に失敗したら再入力
                    Console.WriteLine("無効な入力です。数字を入力してください。\n");
                    continue;
                }
            }
            return countNum;
        }

        static int Count(int num) {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            var count = 0;
            foreach (var n in numbers) {
                if (n == num) {
                    count++;
                }
            }
            return count;
        }

    }
}
