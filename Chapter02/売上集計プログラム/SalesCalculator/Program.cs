using System.Runtime.InteropServices;

namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            var sales = new SalesCounter(@"data\sales.csv"); 
            //ここでリスト化せず集計処理機能にパスを渡して内部でリスト化してもらう方が良い
            var amountsPerStore = sales.GetPerStoreSales();
            foreach (var obj in amountsPerStore) {
                Console.WriteLine($"{obj.Key} {obj.Value}");
            }
        }

    }
}
