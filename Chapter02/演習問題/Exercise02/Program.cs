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
            string input = Console.ReadLine();
            if (input == null) {
                while (input == null) {
                    input = ReadL();
                }
            } else if (input != "1" && input != "2") {
                while (input != "1" && input != "2") {
                    input = ReadL();
                }
            } else { }

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


            if (input == "1") {
                PrintInchToMeterList(start, end);
            } else {
                PrintMeterToInchList(start, end);
            }

            //InchToMeterの結果をConsoleにPrintする
            static void PrintInchToMeterList(int start, int end) {
                for (int inch = start; inch <= end; inch++) {
                    double meter = InchConverter.ToMeter(inch);
                    Console.WriteLine($"{inch}inch = {meter:0.0000}m");
                }
            }

            //InchToMeterの結果をConsoleにPrintする
            static void PrintMeterToInchList(int start, int end) {
                for (int meter = start; meter <= end; meter++) {
                    double inch = InchConverter.FromMeter(meter);
                    Console.WriteLine($"{meter}m = {inch:0.0000}inch");
                }
            }
        }

        public static string ReadL() {
            string Input;
            Console.WriteLine("正しく入力されていません\nもう一度入力を行ってください");
            return Input = Console.ReadLine();
        }
    }
}
