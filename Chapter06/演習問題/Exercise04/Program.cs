namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";
            line.Split(';').ToList().ForEach(s => {
                var keyValue = (s.Split('='));
                if (keyValue.Length == 2) {
                    Console.WriteLine($"{ToJapanese(keyValue[0])}：{keyValue[1]}");
                } else {
                    Console.WriteLine($"無効な形式: {s}");
                }
            });

        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {
            string label = key switch {
                "Novelist" => "作家",
                "BestWork" => "代表作",
                "Born" => "誕生年",
                _ => "引数keyは正しい値ではありません"
            };
            return label;
        }
    }
}