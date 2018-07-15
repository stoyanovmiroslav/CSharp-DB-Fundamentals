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
                string result = GetBooksByPrice(context);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var titlesOfTheGoldenEditionBooks = context.Books.Select(x => new { x.Title, x.Price})
                                                       .Where(x => x.Price > 40).ToList()
                                                       .OrderByDescending(x => x.Price);

            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var t in titlesOfTheGoldenEditionBooks)
            {
                stringBuilder.AppendLine($"{t.Title} - ${t.Price:f2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}