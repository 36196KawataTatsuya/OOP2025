using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01 {
    public class Student {
        //オブジェクト初期化子で設定を強制する
        public string Name { get; init; } = string.Empty;
        public string Subject { get; init; } = string.Empty;
        public int Score { get; init; }
    }
}
