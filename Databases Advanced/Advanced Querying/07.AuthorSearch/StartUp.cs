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

                string result = GetAuthorNamesEndingIn(context, input);
                Console.WriteLine(result);
            }
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {

            var authors = context.Authors
                                 .Where(x => x.FirstName.EndsWith(input))
                                 .Select(x => new
                                 {
                                     FullName = x.FirstName + " " + x.LastName
                                 })
                                 .OrderBy(x => x.FullName).ToList();

            return string.Join(Environment.NewLine, authors.Select(x => x.FullName));
        }
    }
}
