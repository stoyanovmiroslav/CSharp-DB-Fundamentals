using System;
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
                int year = int.Parse(Console.ReadLine());

                string result = GetBooksNotRealeasedIn(context, year);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var titlesOfTheGoldenEditionBooks = context.Books.Select(x => new {x.BookId ,x.Title, x.Price, x.ReleaseDate })
                                                       .Where(x => x.ReleaseDate.Value.Year != year).ToList()
                                                       .OrderBy(x => x.BookId).ToList();


            return string.Join(Environment.NewLine, titlesOfTheGoldenEditionBooks.Select(x => x.Title));
        }
    }
}