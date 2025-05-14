namespace Section01 {
    internal class Program {

        public delegate bool Judgement(int value); //デリゲートの宣言

        static void Main(string[] args) {
            Judgement judge = IsOdd;
            Console.WriteLine(Count(InputArray(), judge)); //入力した数値を配列から検索して個数を出力

        }

        static int[] InputArray() {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            return numbers;
        }

        static bool IsEven(int n) { //偶数を検索する条件を設定
            return n % 2 == 0;
        }

        static bool IsOdd(int n) { //奇数を検索する条件を設定
            return n % 2 == 1;
        }

        static int Count(int[] numbers, Judgement judge) {
            var count = 0;
            foreach (var n in numbers) {
                if (judge(n) == true) {
                    count++;
                }
            }
            return count;
        }

    }
}
