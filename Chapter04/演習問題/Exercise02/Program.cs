namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1();
            Console.WriteLine("---");
            Exercise2();
            Console.WriteLine("---");
            Exercise3();
        }

        private static void Exercise1() {
            int number;
            while (true) {
                var input = Console.ReadLine();
                if (!int.TryParse(input, out number)) {
                    Console.WriteLine("入力値に誤りがあります");
                } else {
                    break;
                }
            }

            if (number < 0) {
                Console.WriteLine(number);
            } else if (/*0 <= number && */number < 100) { //0から99は2倍
                Console.WriteLine(number * 2);
            } else if (/*100 <= number && */number < 500) { //100から499は3倍
                Console.WriteLine(number * 3);
            } else {
                Console.WriteLine(number);
            }
        }

        private static void Exercise2() {
            int number;
            while (true) {
                var input = Console.ReadLine();
                if (!int.TryParse(input, out number)) {
                    Console.WriteLine("入力値に誤りがあります");
                } else {
                    break;
                }
            }

            switch (number) {
                case < 0:
                    Console.WriteLine(number);
                    break;
                case < 100:
                    Console.WriteLine(number * 2);
                    break;
                case < 500:
                    Console.WriteLine(number * 3);
                    break;
                default:
                    Console.WriteLine(number);
                    break;
            }
        }

        private static void Exercise3() {
            int number;
            while (true) {
                var input = Console.ReadLine();
                if (!int.TryParse(input, out number)) {
                    Console.WriteLine("入力値に誤りがあります");
                } else {
                    break;
                }
            }

            var result = number switch {
                < 0 => number,
                < 100 => number * 2,
                < 500 => number * 3,
                _ => number
            };

            Console.WriteLine(result);
        }

    }
}
