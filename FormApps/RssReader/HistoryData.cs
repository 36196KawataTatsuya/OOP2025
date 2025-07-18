using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader {
    public class HistoryData {
        public List<string> UrlHistory { get; set; } = new List<string>();
        public List<string> FavoriteName { get; set; } = new List<string>();
    }
}
