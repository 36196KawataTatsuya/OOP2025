using System.Runtime.CompilerServices;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine("\n(2)");
            Exercise1_3();
            Console.WriteLine("\n(3)");
            Exercise1_4();
            Console.WriteLine("\n(4)");
            Exercise1_5();
            Console.WriteLine("\n(5)");
            Exercise1_6();
            Console.WriteLine("\n(6)");
            Exercise1_7();
            Console.WriteLine("\n(7)");
            Exercise1_8();

            Console.ReadLine();

        }

        private static void Exercise1_2() {
            Console.WriteLine(Library.Books.MaxBy(b => b.Price));
        }

        private static void Exercise1_3() {
            var groups = Library.Books.GroupBy(b => b.PublishedYear);
            foreach (var group in groups) {
                Console.WriteLine($"{group.Key}年：{group.Count()}冊");
            }
            // Library.Books.GroupBy(b => b.PublishedYear)
            // .ToList().ForEach(b => Console.WriteLine($"{b.Key}年：{b.Count()}冊"));
        }

        private static void Exercise1_4() {
            var groups = Library.Books.OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price);
            foreach (var group in groups) {
                Console.WriteLine($"{group.PublishedYear}年 {group.Price}円 {group.Title}");
            }
        }

        private static void Exercise1_5() {
            var books2022 = Library.Books.Where(b => b.PublishedYear == 2022);
            var categoryNames = books2022.Join(
                Library.Categories,
                book => book.CategoryId,
                category => category.Id,
                (book, category) => category.Name
            )
            .Distinct()
            .OrderBy(categoryNames => categoryNames);

            foreach (var categoryName in categoryNames) {
                Console.WriteLine(categoryName);
            }
        }

        private static void Exercise1_6() {
            var groupedBooks = Library.Books.Join(
                Library.Categories,
                book => book.CategoryId,
                category => category.Id,
                (book, category) => new {
                    CategoryName = category.Name,
                    Book = book
                })
            .GroupBy(x => x.CategoryName)
            .OrderBy(g => g.Key);

            foreach (var group in groupedBooks) {
                Console.WriteLine($"# {group.Key}");
                foreach (var item in group) {
                    Console.WriteLine($" {item.Book.Title}");
                }
            }
        }

        private static void Exercise1_7() {
            var categoryGroup = Library.Books.GroupBy(b => b.CategoryId);
            var developmentBooks = categoryGroup
                .Where(g => g.Key == 1)
                .SelectMany(g => g);
            var groupedByYear = developmentBooks
                .GroupBy(b => b.PublishedYear)
                .OrderBy(g => g.Key);
            foreach (var yearGroup in groupedByYear) {
                Console.WriteLine($"# {yearGroup.Key}");

                foreach (var book in yearGroup) {
                    Console.WriteLine($" {book.Title}");
                }
            }
        }

        private static void Exercise1_8() {
            var categoryBookGroups = Library.Categories.GroupJoin(
                Library.Books,
                category => category.Id,
                book => book.CategoryId,
                (category, books) => new {
                    CategoryName = category.Name,
                    BookCount = books.Count()
                });
            var highVolumeCategories = categoryBookGroups
                .Where(item => item.BookCount >= 4);
            foreach (var item in highVolumeCategories) {
                Console.WriteLine($"{item.CategoryName}");
            }
        }

    }
}
