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
                string input = Console.ReadLine();

                string result = GetBooksByCategory(context, input);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] args = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToLower()).ToArray();

            var titlesOfTheGoldenEditionBooks = context.Books
                                                       .Select(x => new
                                                       {
                                                           x.BookId,
                                                           x.Title,
                                                           x.Price,
                                                           x.ReleaseDate,
                                                           BookCategories = x.BookCategories.Select(b => new { b.Category.Name }).ToList()
                                                       })
                                                       .Where(x => x.BookCategories.Any(c => args.Contains(c.Name.ToLower())))
                                                       .OrderBy(x => x.Title).ToList();


            return string.Join(Environment.NewLine, titlesOfTheGoldenEditionBooks.Select(x => x.Title));
        }
    }
}