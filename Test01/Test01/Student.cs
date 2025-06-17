using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01 {
    public class Student {
        //オブジェクト初期化子で設定を強制する
        private string Name { get; set; } = string.Empty;
        private string Subject { get; set; } = string.Empty;
        private int Score { get; set; }
    }
}
