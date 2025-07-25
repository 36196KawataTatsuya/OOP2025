using System.Collections.Generic;

namespace RssReader {
    public class AppSettings {
        public Dictionary<string, string> FavoriteUrls { get; set; } = new Dictionary<string, string>();
        public string BackgroundColorHtml { get; set; } = "#F0F0F0";
    }
}