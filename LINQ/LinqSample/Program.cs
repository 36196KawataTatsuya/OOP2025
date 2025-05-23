namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {

            var numbers = Enumerable.Range(1, 10);

            //合計値を出力
            Console.WriteLine(numbers.Sum());
            Console.WriteLine(numbers.Average());
            Console.WriteLine(numbers.Max());
            Console.WriteLine(numbers.Min());
            Console.WriteLine("----------");
            Console.WriteLine(numbers.Where(x => x % 2 == 0).Sum());
            Console.WriteLine(numbers.Where(x => x % 2 == 1).Sum());
            Console.WriteLine(numbers.Sum(x => x % 2 == 0 ? x : 0)); 

        }
    }
}
