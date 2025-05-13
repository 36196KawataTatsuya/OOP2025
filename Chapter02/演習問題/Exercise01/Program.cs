using System.Security.Cryptography.X509Certificates;

namespace Exercise01 {
    public class Program {

        //2.1.3
        static void Main(string[] args) {
            var songs = new Song[] {
                new Song("Let it be", "The Beatles", 243),
                new Song("Bridge Over Troubled Water", "Simon & Garfunkel", 293),
                new Song("Close To You", "Carpenters", 276),
                new Song("Honesty", "Billy Joel", 231),
                new Song("I Will Always Love You", "Whitney Houston", 273)
            };
            printSongs(songs);
        }

        //2.1.4
        private static void printSongs(Song[] songs) {
            Console.WriteLine("演目一覧\n");
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
