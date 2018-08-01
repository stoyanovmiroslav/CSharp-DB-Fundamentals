namespace ProductShop.ImportData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;

    using Data;
    using Models;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            ImportUsersRecords(context);

            ImportProductsRecords(context);

            ImportCategoriesRecords(context);

            ImportCategoryProductsRecords(context);
        }

        private static void ImportCategoryProductsRecords(ProductShopContext context)
        {
            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            Random random = new Random();

            for (int productId = 1; productId <= 200; productId++)
            {
                var categoryProduct = new CategoryProduct
                {
                    CategoryId = random.Next(1, 12),
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategoriesRecords(ProductShopContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/categories.json");

            var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProductsRecords(ProductShopContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/products.json");

            var products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            Random random = new Random();

            foreach (var product in products)
            {
                product.SellerId = random.Next(1, 57);

                bool isBuyerExist = random.Next(1, 5) == 4;

                if (isBuyerExist)
                {
                    product.BuyerId = random.Next(1, 57);
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsersRecords(ProductShopContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/users.json");

            var users = JsonConvert.DeserializeObject<List<User>>(jsonString);

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}