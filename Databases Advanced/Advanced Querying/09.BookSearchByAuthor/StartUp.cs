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

                string result = GetBooksByAuthor(context, input);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                                 .Select(x => new
                                 {
                                     x.BookId,
                                     x.Title,
                                     x.Author.FirstName,
                                     x.Author.LastName
                                 })
                                 .Where(x => x.LastName.ToLower().StartsWith(input.ToLower()))
                                 .OrderBy(x => x.BookId).ToList();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var b in books)
            {
                stringBuilder.AppendLine($"{b.Title} ({b.FirstName} {b.LastName})");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}