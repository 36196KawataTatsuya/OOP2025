using System;
using System.Collections.Generic;

namespace PrefCapitalLocationSystem {
    internal class Program {
        static private Dictionary<string, string> prefOfficeDict = new Dictionary<string, string>();
        static void Main(string[] args) {
            String? pref, prefCaptalLocation;
            //入力処理
            Console.WriteLine("県庁所在地の登録【入力終了：Ctrl + 'Z'】");
            while (true) {
                //①都道府県の入力
                Console.Write("都道府県:");
                pref = Console.ReadLine();
                if (pref == null) break;    //無限ループを抜ける(Ctrl + 'Z')
                //県庁所在地の入力
                Console.Write("県庁所在地:");
                prefCaptalLocation = Console.ReadLine();

                //既に都道府県が登録されているか？
                if (prefOfficeDict.ContainsKey(pref)) {
                    Console.WriteLine($"「{pref}」は既に登録されています。現在の県庁所在地：{prefOfficeDict[pref]}");
                    Console.Write("上書きしますか？(Y/N):");
                    string? answer = Console.ReadLine();
                    if (answer?.ToUpper() != "Y") {
                        continue; // もう一度都道府県の入力…①へ
                    }
                }

                //県庁所在地登録処理
                if (prefCaptalLocation != null) {
                    prefOfficeDict[pref] = prefCaptalLocation;
                    Console.WriteLine($"「{pref}」の県庁所在地「{prefCaptalLocation}」を登録しました。");
                }

                Console.WriteLine();//改行
            }

            Boolean endFlag = false;    //終了フラグ（無限ループを抜け出す用）
            while (!endFlag) {
                switch (menuDisp()) {
                    case "1":                        //一覧出力処理
                        allDisp();
                        break;
                    case "2"://検索処理
                        searchPrefCaptalLocation();
                        break;
                    case "9"://無限ループを抜ける
                        endFlag = true;
                        break;
                    default:
                        Console.WriteLine("正しい番号を入力してください。");
                        break;
                }   
            }
            Console.WriteLine("プログラムを終了します。");
        }

        //メニュー表示
        private static string? menuDisp() {
            Console.WriteLine("\n**** メニュー ****");
            Console.WriteLine("1：一覧表示");
            Console.WriteLine("2：検索");
            Console.WriteLine("9：終了");
            Console.Write(">");
            var menuSelect = Console.ReadLine();
            return menuSelect;
        }

        //一覧表示処理
        private static void allDisp() {
            Console.WriteLine("\n==== 県庁所在地一覧 ====");
            if (prefOfficeDict.Count == 0) {
                Console.WriteLine("登録されている都道府県はありません。");
                return;
            }

            foreach (var item in prefOfficeDict) {
                Console.WriteLine($"{item.Key} → {item.Value}");
            }
        }

        //検索処理
        private static void searchPrefCaptalLocation() {
            Console.Write("都道府県:");
            String? searchPref = Console.ReadLine();

            if (searchPref == null) {
                Console.WriteLine("都道府県名が入力されていません。");
                return;
            }

            if (prefOfficeDict.ContainsKey(searchPref)) {
                Console.WriteLine($"「{searchPref}」の県庁所在地は「{prefOfficeDict[searchPref]}」です。");
            } else {
                Console.WriteLine($"「{searchPref}」は登録されていません。");
            }
        }
    }
}