
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var numbers = new List<int> { 12, 87, 94, 14, 53, 20, 40, 35, 76, 91, 31, 17, 48 };


            // 3.1.1
            Exercise1(numbers);
            Console.WriteLine("-----");

            // 3.1.2
            Exercise2(numbers);
            Console.WriteLine("-----");

            // 3.1.3
            Exercise3(numbers);
            Console.WriteLine("-----");

            // 3.1.4
            Exercise4(numbers);
        }

        private static void Exercise1(List<int> numbers) {
            /*if (numbers.Exists(n => n % 8 == 0 || n % 9 == 0)) {
                Console.WriteLine("存在しています");
            } else {
                Console.WriteLine("存在しません");
            }*/
            
            /*var exist = numbers.Exists(n => n % 8 == 0 || n % 9 == 0);
            if (exist) {
                Console.WriteLine("存在しています");
            } else {
                Console.WriteLine("存在していません");
            }*/

            if (numbers.Exists(n => n % 8 == 0 || n % 9 == 0)) {
                Console.WriteLine("存在しています");
            } else {
                Console.WriteLine("存在していません");
            }
            
        }

        private static void Exercise2(List<int> numbers) {
            numbers.ForEach(i => Console.WriteLine(i / 2));
        }

        private static void Exercise3(List<int> numbers) {
            numbers.Where(w => w >= 50).ToList().ForEach(j => Console.WriteLine(j));
        }

        private static void Exercise4(List<int> numbers) {
            numbers.Select(l => l * 2).ToList().ForEach(k => Console.WriteLine(k));
        }
    }
}
