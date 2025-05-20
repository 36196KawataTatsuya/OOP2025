namespace Section04 {
    internal class Program {
        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London", "Paris", "Berlin", "Canberra", "Hong Kong"
            };

            var query = cities.Where(s => s.Length <= 5).ToArray();    //- query変数に代入
            foreach (var item in query) { //queryの中身を取り出す
                Console.WriteLine(item); //queryの中身を出力
            }
            Console.WriteLine("------");

            // queryにcitiesの内容を代入したのは前なのにここでcities[0]を更新したのが
            // item in queryの部分にも反映されている

            cities[0] = "Osaka";            //- cities[0]を変更 
            foreach (var item in query) {   //- 再度、queryの内容を取り出す
                Console.WriteLine(item);
            }




        }
    }
}
