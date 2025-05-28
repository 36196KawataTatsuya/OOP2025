using System.Linq;

namespace Section01 {
    public class Program {
        static void Main(string[] args) {

            var books = Books.GetBooks();

            //１．本の平均金額を表示
            Console.WriteLine(books.Average(b => b.Price).ToString("0.00"));
            Console.WriteLine("----------------");

            //２．本のページ合計を表示
            Console.WriteLine(books.Sum(b => b.Pages));
            Console.WriteLine("----------------");

            //３．金額の安い書籍名と金額を表示
            Console.WriteLine(books.OrderBy(b => b.Price).FirstOrDefault()?.Title + " / " + books.Min(b => b.Price));
            Console.WriteLine("----------------");

            books.Where(b => b.Price == books.Min(b => b.Price)).ToList().ForEach(f => Console.WriteLine(f.Title + "/" + f.Price));
            Console.WriteLine("----------------");

            //４．ページが多い書籍名とページ数を表示
            Console.WriteLine(books.OrderByDescending(b => b.Price).FirstOrDefault()?.Title + " / " + books.Max(b => b.Price));
            Console.WriteLine("----------------");

            //５．タイトルに「物語」が含まれている書籍名をすべて表示
            books.Where(b => b.Title.Contains("物語")).ToList().ForEach(f => Console.WriteLine(f.Title));

        }
    }
}
