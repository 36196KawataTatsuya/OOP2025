namespace Test01 {
    public class Program {
        static void Main(string[] args) {
            //ScoreCounterクラスのインスタンスを生成
            var score = new ScoreCounter("./StudentScore.csv");
            //教科ごとの合計点を取得
            var TotalBySubject = score.GetPerStudentScore();
            //合計点を表示
            foreach (var obj in TotalBySubject) {
                Console.WriteLine("{0} {1}", obj.Key, obj.Value);
            }
        }
    }
}

//実行結果
//英語 520
//数学 550
//国語 500