
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            int[] numbers = [5, 10, 17, 9, 3, 21, 10, 40, 21, 3, 35];

            Console.WriteLine("7.1.1");
            Exercise1(numbers);
            Console.WriteLine("");

            Console.WriteLine("7.1.2");
            Exercise2(numbers);
            Console.WriteLine("");

            Console.WriteLine("7.1.3");
            Exercise3(numbers);
            Console.WriteLine("");

            Console.WriteLine("7.1.4");
            Exercise4(numbers);
            Console.WriteLine("");

            Console.WriteLine("7.1.5");
            Exercise5(numbers);
        }

        private static void Exercise1(int[] numbers) {
            Console.WriteLine(numbers.Max());
        }

        private static void Exercise2(int[] numbers) {
            numbers.TakeLast(2).ToList().ForEach(n => Console.WriteLine(n));

            /*
             *  numbers.Reverse().Take(2).Reverse().ToList().ForEach(n => Console.WriteLine(n));
             *  
             *  numbers.Skip(numbers.Length - 2).Take(2).ToList().ForEach(n => Console.WriteLine(n));
             *  
             *  foreach (var n in numbers.Skip(numbers.Length - 2)) {
             *  Console.WriteLine(n);
             *  }
             */
        }

        private static void Exercise3(int[] numbers) {
            numbers.Select(n => n.ToString("000")).ToList().ForEach(n => Console.WriteLine(n));
        }

        private static void Exercise4(int[] numbers) {
            numbers.OrderBy(n => n).Take(3).ToList().ForEach(n => Console.WriteLine(n));
        }

        private static void Exercise5(int[] numbers) {
            Console.WriteLine(numbers.Distinct().Count(n => n > 10));
        }

    }
}
