namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var today = new DateTime(2025,7,12);
            var now = DateTime.Now;

            Console.WriteLine($"Today:{today.Month}");
            Console.WriteLine($"Now:{now}");

            //自分の生年月日は何曜日かをプログラムから調べる
            var birthday = new DateTime(2006, 2, 20);
            var dayOfWeek = birthday.DayOfWeek;

            Console.WriteLine($"誕生日の曜日は:{dayOfWeek}");

            //うるう年の判定プログラムを作成する



        }
    }
}
