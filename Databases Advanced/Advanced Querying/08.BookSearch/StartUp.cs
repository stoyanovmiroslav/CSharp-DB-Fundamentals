using System;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Models;

namespace BookShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new BookShopContext())
            {
                string input = Console.ReadLine();

                string result = GetBookTitlesContaining(context, input);
                Console.WriteLine(result);
            }
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {

            var booktitles = context.Books
                                 .Select(x => new
                                 {
                                     x.Title
                                 })
                                 .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                                 .OrderBy(x => x.Title).ToList();

            return string.Join(Environment.NewLine, booktitles.Select(x => x.Title));
        }
    }
}