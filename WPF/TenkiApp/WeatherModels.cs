using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TenkiApp {
    public class LocationData {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Temperature { get; set; } = "-";
        public string WeatherCode { get; set; } = "-";
        public string WindSpeed { get; set; } = "-";

        public string DisplayWeather => ParseWeatherCode(WeatherCode);

        private string ParseWeatherCode(string code) {
            if (int.TryParse(code, out int c)) {
                if (c == 0) return "快晴";
                if (c <= 3) return "晴れ/曇り";
                if (c <= 48) return "霧";
                if (c <= 55) return "霧雨";
                if (c <= 67) return "雨";
                if (c <= 77) return "雪";
                if (c <= 82) return "にわか雨";
                if (c <= 86) return "雪";
                if (c <= 99) return "雷雨";
            }
            return "不明";
        }
    }

    public class OpenMeteoResponse {
        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }
    }

    public class CurrentWeather {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
        [JsonPropertyName("windspeed")]
        public double Windspeed { get; set; }
        [JsonPropertyName("weathercode")]
        public int Weathercode { get; set; }
    }

    public class GeoSearchResponse {
        [JsonPropertyName("results")]
        public List<GeoResult> Results { get; set; }
    }

    public class GeoResult {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class IpGeoResponse {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }
}