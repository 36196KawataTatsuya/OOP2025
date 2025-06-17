using System.Collections.Generic;

namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            var lines = File.ReadAllLines(filePath);
            _score = lines.Select(line => line.Split(','));


        }

        //メソッドの概要： 
        private static IEnumerable<Student> ReadScore(string filePath) {





        }

        //メソッドの概要： 
        public IDictionary<string, int> GetPerStudentScore() {





        }
    }
}
