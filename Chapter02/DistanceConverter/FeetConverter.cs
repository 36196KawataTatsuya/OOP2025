using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    public static class FeetConverter {

        //定数
        private const double ratio = 0.3048;
        //publicにするならstatic readonlyが良い

        //メートルからフィートを求める
        public static double FromMeter(double meter) {
            return meter / ratio;
        }

        //フィートからメートルを求める
        public static double ToMeter(double feet) {
            return feet * ratio;
        }

    }
}
