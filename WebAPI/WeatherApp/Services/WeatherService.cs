using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyWeatherApp.Models;

namespace MyWeatherApp.Services {
    public class WeatherService {
        private readonly HttpClient _http;

        public WeatherService(HttpClient http) {
            _http = http;
        }

        public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon) {
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max&timezone=auto";

            try {
                return await _http.GetFromJsonAsync<WeatherResponse>(url);
            }
            catch {
                return null;
            }
        }
    }
}