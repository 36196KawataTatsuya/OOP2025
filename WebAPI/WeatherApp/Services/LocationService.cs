using System;
using System.Collections.Generic; // List用
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyWeatherApp.Models;
using MyWeatherApp.Utils;

namespace MyWeatherApp.Services {
    public class LocationService {
        private readonly HttpClient _http;
        private readonly Logger _logger;

        public LocationService(HttpClient http, Logger logger) {
            _http = http;
            _logger = logger;
        }

        // 戻り値を List<LocationInfo> に変更
        public async Task<List<LocationInfo>> GetLocationCandidatesAsync(string name) {
            // count=10 に増やして、候補を多く取得する
            var url = $"https://geocoding-api.open-meteo.com/v1/search?name={name}&count=10&language=ja&format=json";

            try {
                var response = await _http.GetFromJsonAsync<GeoResponse>(url);
                if (response?.Results != null && response.Results.Any()) {
                    _logger.LogInfo($"地域検索成功: {response.Results.Count}件ヒット - クエリ: {name}");

                    // 結果をすべてリストに変換して返す
                    return response.Results.Select(r => new LocationInfo {
                        Name = r.Name,
                        Country = r.Country,
                        Admin1 = r.Admin1, // 県名をセット
                        Latitude = r.Latitude,
                        Longitude = r.Longitude
                    }).ToList();
                } else {
                    _logger.LogInfo($"地域検索結果なし: {name}");
                }
            }
            catch (Exception ex) {
                _logger.LogError($"地域名 '{name}' の取得中にエラーが発生しました。", ex);
            }

            // エラーまたは結果なしの場合は空のリストを返す
            return new List<LocationInfo>();
        }
    
        public async Task<LocationInfo> GetLocationByIpAsync() {
            var url = "http://ip-api.com/json/?fields=status,country,city,regionName,lat,lon";

            try {
                var response = await _http.GetFromJsonAsync<IpResponse>(url);

                if (response?.Status == "success") {
                    _logger.LogInfo($"IP位置特定成功: {response.City}, {response.RegionName}");

                    return new LocationInfo {
                        Name = response.City,
                        Country = response.Country,
                        Admin1 = response.RegionName,
                        Latitude = response.Lat,
                        Longitude = response.Lon
                    };
                } else {
                    _logger.LogInfo($"IP位置特定失敗 ステータス: {response?.Status}");
                }
            }
            catch (Exception ex) {
                _logger.LogError("IPアドレスによる位置特定中にエラーが発生しました。", ex);
            }

            return null;
        }
    }
}