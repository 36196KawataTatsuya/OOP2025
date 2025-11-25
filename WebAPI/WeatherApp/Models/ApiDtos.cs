using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWeatherApp.Models {
    // --- Open-Meteo Geocoding API ---
    public class GeoResponse {
        [JsonPropertyName("results")]
        public List<GeoResult> Results { get; set; }
    }

    public class GeoResult {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    // --- IP-API ---
    public class IpResponse {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }

    // --- Open-Meteo Weather API ---
    public class WeatherResponse {
        [JsonPropertyName("current_units")]
        public CurrentUnits CurrentUnits { get; set; }
        [JsonPropertyName("current")]
        public CurrentWeather Current { get; set; }
        [JsonPropertyName("daily")]
        public DailyWeather Daily { get; set; }
    }

    public class CurrentUnits {
        [JsonPropertyName("temperature_2m")]
        public string Temperature { get; set; }
        [JsonPropertyName("relative_humidity_2m")]
        public string Humidity { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public string WindSpeed { get; set; }
    }

    public class CurrentWeather {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }
        [JsonPropertyName("relative_humidity_2m")]
        public int Humidity { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed { get; set; }
        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }

    public class DailyWeather {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }
        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; }
        [JsonPropertyName("temperature_2m_max")]
        public List<double> TempMax { get; set; }
        [JsonPropertyName("temperature_2m_min")]
        public List<double> TempMin { get; set; }
        [JsonPropertyName("precipitation_probability_max")]
        public List<int?> PrecipProb { get; set; }
    }
}