namespace MyWeatherApp.Utils {
    public static class WeatherCodeUtil {
        public static string ParseWmoCode(int code) {
            return code switch {
                0 => "快晴",
                1 => "晴れ",
                2 => "一部曇り",
                3 => "曇り",
                45 or 48 => "霧",
                51 or 53 or 55 => "霧雨",
                61 or 63 or 65 => "雨",
                66 or 67 => "氷雨",
                71 or 73 or 75 => "雪",
                77 => "あられ",
                80 or 81 or 82 => "にわか雨",
                85 or 86 => "にわか雪",
                95 => "雷雨",
                96 or 99 => "雷雨(雹)",
                _ => "不明"
            };
        }
    }
}