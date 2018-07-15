using System;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new BookShopContext())
            {
                string copiesByAuthor = CountCopiesByAuthor(context);

                Console.WriteLine(copiesByAuthor);
            }
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copiesByAuthor = context.Authors
                                    .Select(x => new
                                    {
                                        FullName = x.FirstName + " " + x.LastName,
                                        Copies = x.Books.Select(c => c.Copies).Sum()
                                    }).OrderByDescending(x => x.Copies).ToList();


            StringBuilder stringBuilder = new StringBuilder();

            foreach (var b in copiesByAuthor)
            {
                stringBuilder.AppendLine($"{b.FullName} - {b.Copies}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}