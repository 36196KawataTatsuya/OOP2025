using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;
            DisplayDatePattern1(dateTime);
            DisplayDatePattern2(dateTime);
            DisplayDatePattern3(dateTime);
        }

        private static void DisplayDatePattern1(DateTime dateTime) {
            // 2024/03/09 19:03
            // string.Formatを使った例
            var formattedDate = string.Format("{0:yyyy/MM/dd HH:mm}",dateTime);
            Console.WriteLine(formattedDate);
        }

        private static void DisplayDatePattern2(DateTime dateTime) {
            // 2024年03月09日 19時03分09秒
            // DateTime.ToStringを使った例
            var formattedDate = dateTime.ToString("yyyy年MM月dd日 HH時mm分ss秒");
            Console.WriteLine(formattedDate);
        }

        private static void DisplayDatePattern3(DateTime dateTime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            // 令和6年03月09日 19時03分09秒
            var formattedDate = string.Format(culture, "{0:ggyy年MM月dd日 HH時mm分ss秒}", dateTime);
            Console.WriteLine(formattedDate);
        }







    }
}

