﻿namespace Section02 {
    internal class Program {
        static void Main(string[] args) {

            var appVer1 = new AppVersion(5, 1);
            var appVer2 = new AppVersion(5, 1);

            Console.WriteLine(appVer1);
            /*if (appVer1 == appVer2) {
                Console.WriteLine("等しい");
            } else {
                Console.WriteLine("等しくない");
            }*/
        }
    }

    //プライマリーコンストラクタを使ったクラス定義
    public record AppVersion (int m, int mi, int b = 0, int r= 0);

        /*public override string ToString() =>
            $"{Major}.{Minor}.{Build}.{Revision}";*/


    }
}
