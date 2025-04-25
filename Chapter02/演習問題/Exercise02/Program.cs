using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("*** 変換アプリ ***");
            Console.WriteLine("1：ヤードからメートル");
            Console.WriteLine("2：メートルからヤード");
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

            Console.Write("変換前(インチ)：");

            while (true) {
                if (int.TryParse(Console.ReadLine(), out int InchVal)) {
                    PrintYardToMeterList(InchVal);
                    break;
                } else {
                    Console.WriteLine("正しく数値を入力してください");
                } 
            }


            if (input == "2") {
                    Console.Write("変換前(メートル)：");
                    PrintMeterToYardList(int.Parse(Console.ReadLine()));
                }
                //YardToMeterの結果をConsoleにPrintする
                static void PrintYardToMeterList(int Yard) {
                    double meter = YardConverter.ToMeter(Yard);
                    Console.WriteLine($"変換後(メートル)：{meter:0.0000}");
                }

            //MeterToYardの結果をConsoleにPrintする
            static void PrintMeterToYardList(int meter) {
                    double Yard = YardConverter.FromMeter(meter);
                    Console.WriteLine($"変換後(ヤード)：{Yard:0.0000}");
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
