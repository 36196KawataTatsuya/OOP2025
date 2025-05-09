using System.Runtime.InteropServices;

namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            SalesCounter sales = new SalesCounter(@"data\sales.csv"); 
            //ここでリスト化せず集計処理機能にパスを渡して内部でリスト化してもらう方が良い
            IDictionary<string, int> amountsPerStore = sales.GetPerStoreSales();
            foreach (KeyValuePair<string, int> obj in amountsPerStore) {
                Console.WriteLine($"{obj.Key} {obj.Value}");
            }
        }

    }
}
