using System.Security.Cryptography.X509Certificates;

namespace Exercise01 {
    public class Program {

        //2.1.3
        static void Main(string[] args) {
            Console.WriteLine("***** 曲の登録 *****");
            List<Song> songs = writeSongs();
            printSongs(songs);
        }

        private static List<Song> writeSongs() {
            var songs = new List<Song>();
            int length = 0;

            while (true) {
                Console.Write("曲名：");
                var Title = Console.ReadLine();
                if (Title == "end") {
                    break;
                }
                while (string.IsNullOrWhiteSpace(Title)) {
                    Console.WriteLine("入力内容が不正です\n再度入力してください\n\n");
                    Console.Write("曲名：");
                    Title = Console.ReadLine();
                }
                if (Title == "end") {
                    break;
                }
                Console.Write("アーティスト名：");
                var ArtistName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(ArtistName)) {
                    Console.WriteLine("入力内容が不正です\n再度入力してください\n\n");
                    Console.Write("アーティスト名：");
                    ArtistName = Console.ReadLine();
                }
                while (true) {
                    Console.Write("演奏時間(秒)：");
                    string? lengthString = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(lengthString)) {
                        Console.WriteLine("入力内容が不正です\n再度入力してください\n\n");
                        Console.Write("演奏時間(秒)：");
                        lengthString = Console.ReadLine();
                    }
                    if (!int.TryParse(lengthString, out length)) {
                        Console.WriteLine("数値として認識できませんでした\n再度入力してください\n");
                        continue;
                    }
                    if (length < 0) {
                        Console.WriteLine("負の数値を演奏時間にすることはできません\n再度入力してください\n");
                        continue;
                    }
                    break;
                }
                songs.Add(new Song(Title, ArtistName, length));
            }
            return songs;
        }




        //2.1.4
        private static void printSongs(List<Song> songs) {
            if (songs.Count == 0) {
                Console.WriteLine("データが1件も入力されていません\nそのままアプリケーションを終了します");
            } else {
                Console.WriteLine("---------------------");
                Console.WriteLine("\n演目一覧\n");
                int timeTotal = 0;
                foreach (var song in songs) {
                    var playTime = TimeSpan.FromSeconds(song.Length);
                    Console.WriteLine($"タイトル / {song.Title}");
                    Console.WriteLine($"アーティスト / {song.ArtistName}");
                    Console.WriteLine($"演奏時間 / {playTime.Minutes}:{playTime.Seconds:00}");
                    Console.WriteLine("---------------------");
                    timeTotal = timeTotal + song.Length;
                }
                /*または以下でも可
                 *foreach (var song in songs) {
                 *  var timespanw = TimeSpan.FromSeconds(song.Length);
                 *  Console.WriteLine(@"{0},{1}{2:m\ss}",
                 *      song.Tittle, song.ArtistName, TimeSpan.FromSeconds(song.Length));
                 *  }
                 */
                var totalTime = TimeSpan.FromSeconds(timeTotal);
                Console.WriteLine($"全体演奏時間 / {totalTime.Minutes}：{totalTime.Seconds:00}");
            }
        }
    }
}
