using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class InchConverter {

        //変換する為の定数
        private const double ratio = 0.0254;

        //インチからメートルへ変換して返す
        public static double ToMeter(double Inch) {
            return Inch * ratio;
        }

        //メートルからインチへ変換して返す
        public static double FromMeter(double Meter) {
            return Meter / ratio;
        }

    }
}
