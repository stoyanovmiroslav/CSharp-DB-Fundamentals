namespace BookShop
{
    using System;
    using System.Linq;
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);
            }
        }
    }
}