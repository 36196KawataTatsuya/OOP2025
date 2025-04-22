namespace DistanceConverter {
    internal class Program {
        static void Main(string[] args) {
            /* -tom << メートルへ変換
             * -tof <, フィートへ変換  */

            //if (args.Length >= 1 && args[0] == "-tom" ... のような指定あるとなお良し。

            int arg2 = 0;
            int arg3 = 0;

            if (args.Length == 2) {
                arg2 = int.Parse(args[1]);
            } else if (args.Length == 3) {
                arg2 = int.Parse(args[1]);
                arg3 = int.Parse(args[2]);
            } else {
            }

            if (args.Length == 0) { //args[0]が無い場合
                Console.WriteLine("例外が発生しました\nargs[0]に何も指定されていません");

                //-tom -tofの振り分けとargs.Lengthの判定
            } else if (args.Length == 1 && args[0] == "-tom") {
                PrintFeetToMeterList(1, 10);

            } else if (args.Length == 1 && args[0] == "-tof") {
                PrintMeterToFeetList(1, 10);

            } else if (args.Length == 2 && args[0] == "-tom") {
                PrintFeetToMeterList(arg2, arg2 + 10);

            } else if (args.Length == 2 && args[0] == "-tof") {
                PrintMeterToFeetList(arg2, arg2 + 10);

            } else if (args.Length == 3 && args[0] == "-tom") {
                PrintFeetToMeterList(arg2, arg3);

            } else if (args.Length == 3 && args[0] == "-tof") {
                PrintMeterToFeetList(arg2, arg3);

            } else if (args.Length >= 4) {  //引数が4つ以上指定された場合
                Console.WriteLine("例外が発生しました\n引数が多すぎます");

            } else {    //非対応の引数が指定された場合
                Console.WriteLine("例外が発生しました\n無効な引数です");
            }

            //FeetToMeterの結果をConsoleにPrintする
            static void PrintFeetToMeterList(int start, int end) {
                FeetConverter converter = new FeetConverter();

                for (int feet = start; feet <= end; feet++) {
                    double meter = converter.ToMeter(feet);
                    Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                }
            }

            //MeterToFeetの結果をConsoleにPrintする
            static void PrintMeterToFeetList(int start, int end) {
                FeetConverter converter = new FeetConverter();

                for (int meter = start; meter <= end; meter++) {
                    double feet = converter.FromMeter(meter);
                    Console.WriteLine($"{meter}m = {feet:0.0000}ft");
                }
            }

        }
    }
}
