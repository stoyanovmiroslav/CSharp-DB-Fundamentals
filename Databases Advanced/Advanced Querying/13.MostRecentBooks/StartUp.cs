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
                string result = GetMostRecentBooks(context);

                Console.WriteLine(result);
            }
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var mostRecentBooks = context.Categories
                                    .Select(x => new
                                    {
                                        x.Name,
                                        Books = x.CategoryBooks.Select(b => b.Book).OrderByDescending(b => b.ReleaseDate).Take(3).ToList()
                                    }).OrderBy(x => x.Name).ToList();


            StringBuilder stringBuilder = new StringBuilder();

            foreach (var category in mostRecentBooks)
            {
                stringBuilder.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    stringBuilder.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}