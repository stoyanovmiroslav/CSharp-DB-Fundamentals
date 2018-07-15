using System;
using System.Linq;
using BookShop.Data;
using BookShop.Models;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new BookShopContext())
            {
                string result = GetGoldenBooks(db);
                Console.WriteLine(result);
            }

        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var titlesOfTheGoldenEditionBooks = context.Books.Select(x => new { x.BookId, x.EditionType, x.Title, x.Copies })
                                                       .Where(x => x.Copies < 5000).ToList()
                                                       .Where(x => x.EditionType == EditionType.Gold)
                                                       .OrderBy(x => x.BookId);

            return string.Join(Environment.NewLine, titlesOfTheGoldenEditionBooks.Select(x => x.Title));
        }
    }
}
