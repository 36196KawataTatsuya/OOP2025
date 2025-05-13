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
                new Song("I Will Always Love You", "Whitney Houston", 273),
            };
            printSongs(songs);
        }

        //2.1.4
        private static void printSongs(Song[] songs) {
            foreach (var song in songs) {
                int min = song.Length / 60;
                int sec = song.Length % 60;
                Console.WriteLine($"タイトル / {song.Title}");
                Console.WriteLine($"アーティスト / {song.ArtistName}");
                Console.WriteLine($"演奏時間 / {min}:{sec:00}");
                Console.WriteLine("\n");
            }

        }

    }
}
    