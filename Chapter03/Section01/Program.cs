namespace Section01 {
    internal class Program {

        static void Main(string[] args) {
            /*  Actionデリゲート
             *      戻り値：なし
             *      Action<T>
             *          Tは引数の型
             *      例：Action<int>
             *  
             *  Predicateデリゲート
             *      戻り値：bool
             *      Predicate<T>
             *          Tは引数の型で、戻り値はbool型
             *
             *  Funcデリゲート
             *      戻り値：TResultで指定した型
             *      Func<T, TResult>
             *          Tは引数の型
             */
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };

            Console.WriteLine(Count(numbers, n => /*5,6,7,8,9*/ 5 <= n && n < 10));
            //var numbers に入った数値が numbers, n =>... のnに入る


        }

        static int Count(int[] numbers, Predicate<int> judge) {
            var count = 0;
            foreach (var v in numbers) {
                //引数で受け取ったメソッドを呼び出す
                if (judge(v) == true) {
                    count++;
                }
            }
            return count;
        }
        
    }
}