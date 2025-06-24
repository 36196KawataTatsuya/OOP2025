using System.Globalization;
using System.Runtime.CompilerServices;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            int year = 0;
            int month = 0;
            int day = 0;

            while (true) {
                Console.Write("年を入力してください:");
                if (int.TryParse(Console.ReadLine(), out year)) {
                    if (year > 0) {
                        break;
                    } else {
                        Console.WriteLine("正しい年を入力してください");
                    }
                } else {
                    Console.WriteLine("数字を入力してください");
                }
            }
            while (true) {
                Console.Write("月を入力してください:");
                if (int.TryParse(Console.ReadLine(), out month)) {
                    if (12 >= month && month >= 1) {
                        break;
                    } else {
                        Console.WriteLine("正しい月を入力してください");
                    }
                } else {
                    Console.WriteLine("数字を入力してください");
                }
            }
            while (true) {
                Console.Write("日を入力してください:");
                if (int.TryParse(Console.ReadLine(), out day)) {
                    if (1 <= day && day <= DateTime.DaysInMonth(year, month)) {
                        break;
                    } else {
                        Console.WriteLine("正しい日を入力してください");
                    }
                } else {
                    Console.WriteLine("数字を入力してください");
                }
            }

            var birthday = new DateTime(year, month, day);
            var now = DateTime.Now;

            Console.WriteLine($"Today Month:{birthday.Month}");
            Console.WriteLine($"Now Date:{now}");

            Console.WriteLine();

            //自分の生年月日は何曜日かをプログラムから調べる
            var dayOfWeek = birthday.DayOfWeek;
            string[] dOWString = { "日曜日", "月曜日", "火曜日", "水曜日", "木曜日", "金曜日", "土曜日" };
            Console.WriteLine("自己流での曜日判定");
            Console.WriteLine($"誕生日の曜日は:{dOWString[(int)dayOfWeek]}");

            Console.WriteLine();

            var culture = new CultureInfo("ja-JP");
            var dayOfWeekJP = culture.DateTimeFormat.GetDayName(dayOfWeek);
            Console.WriteLine("教科書通りの曜日判定");
            Console.WriteLine($"誕生日の曜日は:{dayOfWeekJP}");

            Console.WriteLine();

            //うるう年の判定プログラムを作成する
            var isLeapYear = DateTime.IsLeapYear(birthday.Year);
            if (isLeapYear) {
                Console.WriteLine("うるう年です");
            } else {
                Console.WriteLine("うるう年ではありません");
            }

            Console.WriteLine();

            //生まれてからの日数を求める
            var daysSinceBirth = (now - birthday).Days;
            Console.WriteLine($"生まれてからの日数は:{daysSinceBirth}日");

            //年齢を求める
            var age = now.Year - birthday.Year;
            if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day)) {
                age--; // 誕生日がまだ来ていない場合は1年引く
            }
            Console.WriteLine($"年齢は:{age}歳");
            Console.WriteLine();

            //生まれてからの秒数を求める
            //TimeSpan diff;
            //var birth = new DateTime(year, month, day);
            //diff = DateTime.Now - birth;
            //Console.WriteLine($"\r{diff.TotalSeconds}");
            //Console.WriteLine();


            while (true) {
                // キーが押されているかチェック
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter) {
                        break;
                    }
                }

                now = DateTime.Now;
                Console.Write($"\rNow Date:{now}");
                Thread.Sleep(100); // 待機
            }
            Console.WriteLine("\nプログラムを終了しました\n\n\n\n\n");

        }
    }
}
