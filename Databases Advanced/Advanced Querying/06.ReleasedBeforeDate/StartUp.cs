using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Models;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new BookShopContext())
            {
                string date = Console.ReadLine();

                string result = GetBooksReleasedBefore(context, date);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {

            DateTime convertedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var books = context.Books
                               .Select(x => new
                               {
                                   x.Title,
                                   x.Price,
                                   x.ReleaseDate,
                                   x.EditionType
                               })
                               .Where(x => x.ReleaseDate < convertedDate)
                               .OrderByDescending(x => x.ReleaseDate).ToList();

            return string.Join(Environment.NewLine, books.Select(x => x.Title + " - " + x.EditionType + " - $" + x.Price.ToString("0.00")));
        }
    }
}
