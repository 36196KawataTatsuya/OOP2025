using System.Collections.Generic;

namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);
        }

        //メソッドの概要： 
        private static IEnumerable<Student> ReadScore(string filePath) {
            //スコアデータを入れるリストオブジェクトを生成
            var students = new List<Student>();
            //ファイルを一気に読み込み
            var lines = File.ReadAllLines(filePath);
            //読み込んだ行数分繰り返し
            foreach (string line in lines) {
                var items = line.Split(',');
                //Studentオブジェクトを生成 
                var student = new Student() {
                    Name = items[0],
                    Subjects = items[1],
                    Amount = int.Parse(items[2])
                };
                sales.Add(sale);
            }

            return sales;
        }

        //メソッドの概要： 
        public IDictionary<string, int> GetPerStudentScore() {





        }
    }
}
