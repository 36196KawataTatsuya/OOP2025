using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class YardConverter {

        //変換する為の定数
        private const double ratio = 0.9144;

        //ヤードからメートルへ変換して返す
        public static double ToMeter(double Yard) {
            return Yard * ratio;
        }

        //メートルからヤードへ変換して返す
        public static double FromMeter(double Meter) {
            return Meter / ratio;
        }

    }
}
