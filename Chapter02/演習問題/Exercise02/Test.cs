using System.Security;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            //適当な文字で初期化
            Char mode = 'A';
            string inputString = "A";
            int inputInt = 0;

            //モードを選択
            Console.WriteLine("モードを選択してください\nインチからメートルへの変換表を出力する場合は M\nメートルからインチへの変換表を出力する場合は I を入力してください");
            inputString = Console.ReadLine();
            if (inputString.Length == 1) {
                mode = char.Parse(inputString);
            }

            //適切でないモード選択の場合再度入力を促す
            while (mode != 'M' && mode != 'I') {
                Console.WriteLine("有効なモードが選択されませんでした\n再度入力を行ってください");
                inputString = Console.ReadLine();
                mode = char.Parse(inputString);
            }

            //どこまで表示させるかを決定させる
            if (mode == 'M') {
                Console.WriteLine("何インチまでの変換表を出力するかを決定してください\n2以上の数字を入力してください");
                inputInt = int.Parse(Console.ReadLine());
                while (1! < inputInt) {
                    Console.WriteLine("2以上の数字で再入力してください");
                    inputInt = int.Parse(Console.ReadLine());
                }
            } else if (mode == 'I') {
                Console.WriteLine("何メートルまでの変換表を出力するかを決定してください\n2以上の数字を入力してください");
                inputInt = int.Parse(Console.ReadLine());
                while (1! < inputInt) {
                    Console.WriteLine("2以上の数字で再入力してください");
                    inputInt = int.Parse(Console.ReadLine());
                }

                //入力された数値をmax値に代入
                int max = inputInt;

                //モード別に振り分けて実行
                if (mode == 'M') {
                    for (int inch = 1; inch <= max; inch++) {
                        double meter = InchConverter.ToMeter(inch);
                        Console.WriteLine($"{inch}inch = {meter:0.0000}m");
                    }
                } else if (mode == 'I') {
                    for (int meter = 1; meter <= max; meter++) {
                        double inch = InchConverter.FromMeter(meter);
                        Console.WriteLine($"{meter}m = {inch:0.0000}inch");
                    }
                }


            }
        }
    }
}
