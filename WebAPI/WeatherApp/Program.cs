using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyWeatherApp.Models;
using MyWeatherApp.Services;
using MyWeatherApp.Utils;

namespace MyWeatherApp {
    internal class Program {
        // HttpClientはアプリケーションライフサイクル全体で共有
        private static readonly HttpClient _http = new HttpClient();

        static async Task Main(string[] args) {
            InitializeHttpClient();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Loggerのインスタンス生成
            var logger = new Logger();

            // 2. ServiceにLoggerを渡す
            var locService = new LocationService(_http, logger);
            var weatherService = new WeatherService(_http); // 必要ならWeatherServiceにも渡せます

            logger.LogInfo("アプリケーションを開始しました。");

            while (true) {
                Console.Clear();
                Console.WriteLine("=== 天気情報アプリ ===");
                Console.WriteLine("1. 地域名から検索");
                Console.WriteLine("2. 現在地から取得 (IPアドレスベース)");
                Console.WriteLine("Q. 終了");
                Console.Write("\n選択してください: ");

                var choice = Console.ReadLine();
                if (choice?.ToUpper() == "Q") break;

                LocationInfo location = null;

                Console.WriteLine();

                if (choice == "1") {
                    Console.Write("地域名を入力 (例: Ota Gunma, Tokyo): ");
                    var query = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(query)) {
                        Console.WriteLine("検索中...");

                        // 修正したメソッドを呼び出す
                        var candidates = await locService.GetLocationCandidatesAsync(query);

                        if (candidates.Count == 0) {
                            Console.WriteLine("見つかりませんでした。");
                        } else if (candidates.Count == 1) {
                            // 1件だけならそのまま採用
                            location = candidates[0];
                        } else {
                            // 複数ある場合は選ばせる
                            Console.WriteLine($"\n{candidates.Count} 件の候補が見つかりました。番号を選択してください:");

                            for (int i = 0; i < candidates.Count; i++) {
                                var loc = candidates[i];
                                string regionInfo = string.IsNullOrEmpty(loc.Admin1) ? loc.Country : $"{loc.Admin1}, {loc.Country}";
                                Console.WriteLine($"{i + 1}. {loc.Name} ({regionInfo})");
                            }

                            Console.Write("\n番号を入力 (キャンセルは 0): ");
                            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= candidates.Count) {
                                location = candidates[selectedIndex - 1];
                            } else {
                                Console.WriteLine("選択をキャンセルしました。");
                            }
                        }
                    }
                } else if (choice == "2") {
                    Console.WriteLine("現在地を特定中...");
                    location = await locService.GetLocationByIpAsync();
                }

                if (location != null) {
                    Console.WriteLine($"\n> {location.Name} ({location.Country}) の情報を取得します...");
                    var weather = await weatherService.GetWeatherAsync(location.Latitude, location.Longitude);
                    DisplayWeather(weather);
                } else {
                    Console.WriteLine("場所が見つからないか、エラーが発生しました。");
                }

                Console.WriteLine("\nEnterキーでメニューに戻ります...");
                Console.ReadLine();
            }
        }

        private static void InitializeHttpClient() {
            _http.Timeout = TimeSpan.FromSeconds(10);
            _http.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp/1.0 (Using Open-Meteo API)");
        }

        private static void DisplayWeather(WeatherResponse weather) {
            if (weather?.Current == null || weather.Daily == null) {
                Console.WriteLine("天気データの取得に失敗しました。");
                return;
            }

            Console.WriteLine("\n----------- 現在の天気 -----------");
            Console.WriteLine($"天気　: {WeatherCodeUtil.ParseWmoCode(weather.Current.WeatherCode)}");
            Console.WriteLine($"気温　: {weather.Current.Temperature}{weather.CurrentUnits.Temperature}");
            Console.WriteLine($"湿度　: {weather.Current.Humidity}{weather.CurrentUnits.Humidity}");
            Console.WriteLine($"風速　: {weather.Current.WindSpeed}{weather.CurrentUnits.WindSpeed}");

            Console.WriteLine("\n----------- 週間予報 -----------");
            Console.WriteLine($"{"日付",-14} | {"天気",-10} | {"最高",-6} | {"最低",-6} | {"降水",-6}");
            Console.WriteLine(new string('-', 60));

            var d = weather.Daily;
            // API仕様変更等で配列長が一致しない場合のエラー回避
            int count = Math.Min(d.Time.Count, d.WeatherCode.Count);

            for (int i = 0; i < count; i++) {
                if (!DateTime.TryParse(d.Time[i], out var dateVal)) continue;

                var dateStr = dateVal.ToString("MM/dd (ddd)");
                var wCode = WeatherCodeUtil.ParseWmoCode(d.WeatherCode[i]);
                // 文字数が多い天気コードの場合は省略表記にする
                if (wCode.Length > 8) wCode = wCode.Substring(0, 7) + "…";

                var maxT = $"{d.TempMax[i]}";
                var minT = $"{d.TempMin[i]}";
                var precip = d.PrecipProb[i].HasValue ? $"{d.PrecipProb[i]}%" : "-";

                Console.WriteLine($"{dateStr,-14} | {wCode,-10} | {maxT,-6} | {minT,-6} | {precip,-6}");
            }
        }
    }
}