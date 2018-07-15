using BookShop.Data;
using System;
using System.Linq;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new BookShopContext())
            {
                string command = Console.ReadLine();

                string result = GetBooksByAgeRestriction(db, command);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var bookTitles = context.Books.Select(x => new { x.AgeRestriction, x.Title }).ToList()
                                    .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower()).OrderBy(x => x.Title).ToList();

            return string.Join(Environment.NewLine, bookTitles.Select(x => x.Title));
        }
    }
}