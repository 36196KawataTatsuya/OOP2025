using System;
using System.IO;

namespace MyWeatherApp.Utils {
    public class Logger {
        private readonly string _logFilePath;

        public Logger() {
            // 実行ファイルと同じ場所に logs.txt を作成
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            _logFilePath = Path.Combine(docPath, "app_logs.txt");
        }

        public void LogInfo(string message) {
            WriteLog("INFO", message, ConsoleColor.White);
        }

        public void LogError(string message, Exception ex = null) {
            string formattedMessage = ex == null ? message : $"{message} | Details: {ex.Message}";
            WriteLog("ERROR", formattedMessage, ConsoleColor.Red);

            // エラー時はスタックトレースもファイルに残す（コンソールには出さない）
            if (ex != null) {
                File.AppendAllText(_logFilePath, $"\tStack Trace: {ex.StackTrace}{Environment.NewLine}");
            }
        }

        private void WriteLog(string level, string message, ConsoleColor color) {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logEntry = $"[{timestamp}] [{level}] {message}";

            // 1. コンソールに出力 (色付き)
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(logEntry);
            Console.ForegroundColor = originalColor;

            // 2. ファイルに出力
            try {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ioEx) {
                // ログ書き込み自体のエラーはコンソールに出すしかない
                Console.WriteLine($"ログファイル書き込み失敗: {ioEx.Message}");
            }
        }
    }
}