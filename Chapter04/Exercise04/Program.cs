using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            #region nullの判定
            string? name = null;

            if (name is null)
                Console.WriteLine("nameはnullです");
            #endregion

            #region null合体演算子
            string code = "12345";
            var message = GetMessage(code); //?? DefaultMessage();
            message ??= DefaultMessage();
            Console.WriteLine(message);

            #endregion

            #region null条件演算子
            //Sale? sale = new Sale {
            //    ShopName = "新宿店",
            //    ProductCategory = "洋菓子",
            //    Amount = 523100
            //};
            Sale? sale = null;

            int? amount = sale?.Amount;

            Console.WriteLine(amount);

            #endregion

            #region 値の入れ替え
            var a = 10;
            var b = 20;

            Console.WriteLine("a = " + a + " / b = " + b);

            (b, a) = (a, b);
            //var bukkit = a;
            //a = b;
            //b = bukkit;

            Console.WriteLine("a = " + a + " / b = " + b);
            #endregion

            #region
            string? inputData = Console.ReadLine();

            if (int.TryParse(inputData, out var number)) {
                Console.WriteLine(number);
            } else {
                Console.WriteLine("フォーマットエラー");
            }



            /*
            try {
                int num = int.Parse(inputData);
                Console.WriteLine(num);
            }
            catch (FormatException fe) {
                //Console.WriteLine(fe.Message);
                Console.WriteLine("フォーマットエラー");
            }
            catch (OverflowException) {
                Console.WriteLine("入力値が大きすぎます");
            }
            finally {
                Console.WriteLine("処理完了");
            }
            Console.WriteLine("メソッド終了"):
            */
            #endregion


        }





        private static object? GetMessage(string code) {
            return code;
        }

        private static object? DefaultMessage() {
            return "DefaultMessage";
        }


        //売上クラス 
        public class Sale {

            //店舗名
            public string ShopName { get; set; } = String.Empty;
            //商品カテゴリ
            public string ProductCategory { get; set; } = String.Empty;
            //売上高
            public int Amount { get; set; }
        }


    }
}
