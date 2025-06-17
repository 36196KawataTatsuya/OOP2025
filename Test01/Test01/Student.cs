using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01 {
    public class Student {
        //オブジェクト初期化子で設定を強制する
        private string Name { get; init; } = string.Empty;
        private string Subject { get; init; } = string.Empty;
        private int Score { get; init; }
    }
}
