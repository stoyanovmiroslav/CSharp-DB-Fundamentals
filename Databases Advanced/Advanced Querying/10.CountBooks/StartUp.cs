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
                int lengthCheck = int.Parse(Console.ReadLine());

                int booksCount = CountBooks(context, lengthCheck);

                Console.WriteLine($"There are {booksCount} books with longer title than {lengthCheck} symbols");
            }
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books.Where(x => x.Title.Length > lengthCheck).Count();
            return booksCount;
        }
    }
}