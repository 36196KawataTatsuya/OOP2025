using Exercise01;
using System.Linq;

namespace Exercise02 {
    public class Program {
        static void Main(string[] args) {
            // 5.2.1
            var ymCollection = new YearMonth[] {
                new YearMonth(1980, 1),
                new YearMonth(1990, 4),
                new YearMonth(2000, 7),
                new YearMonth(2010, 9),
                new YearMonth(2024, 12),
            };

            Console.WriteLine("5.2.2");
            Exercise2(ymCollection);

            Console.WriteLine("5.2.4");
            Exercise4(ymCollection);

            Console.WriteLine("5.2.5");
            Exercise5(ymCollection);
        }

        private static void Exercise2(YearMonth[] ymCollection) {
            //ymCollection.ToList().ForEach(ym => {
            //    Console.WriteLine($"{ym.Year}年{ym.Month:00}月");
            //});
            ymCollection.ToList().ForEach(ym => { Console.WriteLine(ym); });
        }   

        //5.2.3
        //ここにメソッドを作成 (メソッド名 : FindFirst21C)
        private static YearMonth? FindFirst21C(YearMonth[] ymCollection) {
            //最初に見つかった21世紀のYearMonthオブジェクトを返すメソッド
            foreach (var ym in ymCollection) {
                if (ym.Is21Century) {
                    return ym;
                }
            }
            // return ymCollection.FirstOrDefault(ym => ym.Is21Century);
            return null;
        }

        private static void Exercise4(YearMonth[] ymCollection) {
            var result = FindFirst21C(ymCollection);
            if (result == null) {
                Console.WriteLine("21世紀のデータはありません");
            } else {
                Console.WriteLine($"{result.Year}");
            }
        }

        private static void Exercise5(YearMonth[] ymCollection) {
            YearMonth[] nextMonthCollection = ymCollection.Select(yc => yc.AddOneMonth()).ToArray();
            Array.ForEach(nextMonthCollection, nMC => Console.WriteLine(nMC));
        }

    }
}
