using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("*** 変換アプリ ***");
            Console.WriteLine("1：インチからメートル");
            Console.WriteLine("2：メートルからインチ");
            //モード決定
            string input = Console.ReadLine();
            //null対策 & 指定数字外入力対策
            if (input == null) {
                while (input == null) {
                    input = ReadL();
                }
            } else if (input != "1" && input != "2") {
                while (input != "1" && input != "2") {
                    input = ReadL();
                }
            } else { }
            //スタート値の決定
            int start = 0;
            Console.Write("はじめ：");
            while (true) {
                if (int.TryParse(Console.ReadLine(), out int startInput)) {
                    start = startInput;
                    break;
                } else {
                    Console.WriteLine("数値を入力してください");
                    Console.Write("はじめ：");
                }
            }
            //エンド値の決定
            int end = 0;
            Console.Write("おわり：");
            while (true) {
                if (int.TryParse(Console.ReadLine(), out int endInput)) {
                    end = endInput;
                    break;
                } else {
                    Console.WriteLine("数値を入力してください");
                    Console.Write("おわり：");
                }
            }
            //エンド値がスタート値より小さい場合の再入力機構
            while (end < start) {
                Console.WriteLine("おわりの値をはじめの値より小さく指定することはできません");
                Console.WriteLine("数値を入力してください");
                Console.Write("おわり：");
                while (true) {
                    if (int.TryParse(Console.ReadLine(), out int endInput)) {
                        end = endInput;
                        break;
                    } else {
                        Console.WriteLine("数値を入力してください");
                        Console.Write("おわり：");
                    }
                }
            }
            //モード参照して変換メソッドを起動
            if (input == "1") {
                PrintYardToMeterList(start, end);
            } else {
                PrintMeterToYardList(start, end);
            }

            //YardToMeterの結果をConsoleにPrintする
            static void PrintYardToMeterList(int start, int end) {
                for (int Yard = start; Yard <= end; Yard++) {
                    double meter = YardConverter.ToMeter(Yard);
                    Console.WriteLine($"{Yard}y = {meter:0.0000}m");
                }
            }

            //MeterToYardの結果をConsoleにPrintする
            static void PrintMeterToYardList(int start, int end) {
                for (int meter = start; meter <= end; meter++) {
                    double Yard = YardConverter.FromMeter(meter);
                    Console.WriteLine($"{meter}m = {Yard:0.0000}y");
                }
            }
        }
            //不正入力時の再入力機構
        public static string ReadL() {
            string Input;
            Console.WriteLine("正しく入力されていません\nもう一度入力を行ってください");
            return Input = Console.ReadLine();
        }
    }
}
