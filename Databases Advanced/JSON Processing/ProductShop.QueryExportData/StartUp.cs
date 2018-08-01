using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using System;
using System.IO;
using System.Linq;

namespace ProductShop.QueryExportData
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //Query 1. Products In Range
            ProductsInRange(context);

            //Query 2. Sold Products
            SoldProducts(context);

            //Query 3.Categories By Products Count
            CategoriesByProductsCount(context);

            //Query 4. Users and Products
            UsersWithProducts(context);
        }

        private static void UsersWithProducts(ProductShopContext context)
        {
            var users = context.Users.Where(x => x.ProductsSold.Any(a => a.BuyerId != null))
                                     .Select(x => new
                                     {
                                         firstName = x.FirstName,
                                         lastName = x.LastName,
                                         age = x.Age,
                                         soldProducts = new
                                         {
                                             count = x.ProductsSold.Where(b => b.BuyerId != null).Count(),
                                             products = x.ProductsSold.Where(b => b.BuyerId != null)
                                                          .Select(a => new
                                                          {
                                                              name = a.Name,
                                                              price = a.Price
                                                          }).ToArray()
                                         }
                                     })
                                     .OrderByDescending(x => x.soldProducts.count)
                                     .ToArray();

            var user = new { usersCount = users.Count(), users = users };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var jsonString = JsonConvert.SerializeObject(user, jsonSerializerSettings);

            File.WriteAllText("users-and-products.json", jsonString);
        }

        private static void CategoriesByProductsCount(ProductShopContext context)
        {
            var category = context.Categories.Select(x => new
            {
                category = x.Name,
                productsCount = x.CategoryProducts.Count,
                averagePrice = x.CategoryProducts.Sum(a => a.Product.Price) / x.CategoryProducts.Count,
                totalRevenue = x.CategoryProducts.Sum(s => s.Product.Price)
            })
            .OrderByDescending(x => x.productsCount)
            .ToArray();

            var jsonString = JsonConvert.SerializeObject(category, Formatting.Indented);

            File.WriteAllText("categories-by-products.json", jsonString);
        }

        private static void SoldProducts(ProductShopContext context)
        {
            var users = context.Users.Where(z => z.ProductsSold.Any(x => x.BuyerId != null)).Select(x => new
            {
                firstName = x.FirstName,
                lastName = x.LastName,
                soldProducts = x.ProductsSold.Where(b => b.BuyerId != null).Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    buyerFirstName = p.Buyer.FirstName,
                    buyerLastName = p.Buyer.LastName
                }).ToArray()
            })
            .OrderBy(x => x.lastName)
            .ThenBy(x => x.firstName)
            .ToArray();

            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText("users-sold-products.json", jsonString);
        }

        private static void ProductsInRange(ProductShopContext context)
        {
            var curtomers = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
                                            .OrderBy(x => x.Price)
                                            .Select(x => new
                                            {
                                                name = x.Name,
                                                price = x.Price,
                                                seler = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                                            }).ToArray();

            string jsonString = JsonConvert.SerializeObject(curtomers, Formatting.Indented);

            File.WriteAllText("products-in-range.json", jsonString);
        }
    }
}