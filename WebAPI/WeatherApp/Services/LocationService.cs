using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyWeatherApp.Models;

namespace MyWeatherApp.Services {
    public class LocationService {
        private readonly HttpClient _http;

        public LocationService(HttpClient http) {
            _http = http;
        }

        public async Task<LocationInfo> GetLocationByNameAsync(string name) {
            var url = $"https://geocoding-api.open-meteo.com/v1/search?name={name}&count=1&language=ja&format=json";

            try {
                var response = await _http.GetFromJsonAsync<GeoResponse>(url);
                if (response?.Results != null && response.Results.Any()) {
                    var first = response.Results.First();
                    return new LocationInfo {
                        Name = first.Name,
                        Country = first.Country,
                        Latitude = first.Latitude,
                        Longitude = first.Longitude
                    };
                }
            }
            catch { /* ログ処理などをここに挟む */ }

            return null;
        }

        public async Task<LocationInfo> GetLocationByIpAsync() {
            // IP-APIはhttp (非SSL) のエンドポイントも多いため、環境によってはhttps版の使用を検討してください
            var url = "http://ip-api.com/json/?fields=status,country,city,lat,lon";

            try {
                var response = await _http.GetFromJsonAsync<IpResponse>(url);
                if (response?.Status == "success") {
                    return new LocationInfo {
                        Name = response.City,
                        Country = response.Country,
                        Latitude = response.Lat,
                        Longitude = response.Lon
                    };
                }
            }
            catch { }

            return null;
        }
    }
}