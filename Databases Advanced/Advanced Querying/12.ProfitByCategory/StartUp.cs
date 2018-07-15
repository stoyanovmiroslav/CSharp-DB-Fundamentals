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
                string result = GetTotalProfitByCategory(context);

                Console.WriteLine(result);
            }
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var totalProfitByCategory = context.Categories
                                               .Include(x => x.CategoryBooks)
                                               .Select(x => new
                                               {
                                                   x.Name,
                                                   ProfitByCategory = x.CategoryBooks.Select(b => b.Book.Price * b.Book.Copies).Sum()
                                               }).OrderByDescending(x => x.ProfitByCategory).ToList();


            StringBuilder stringBuilder = new StringBuilder();

            foreach (var c in totalProfitByCategory)
            {
                stringBuilder.AppendLine($"{c.Name} ${c.ProfitByCategory:f2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}