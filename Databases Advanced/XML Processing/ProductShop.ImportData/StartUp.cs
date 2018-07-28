using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Models;
using ProductShopDatabase.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProductShopDatabase
{
    public class StarUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            ImportUsersRecords(context);

            ImportProductsRecords(context);

            ImportCategoriesRecords(context);

            ImportCategoryProductsRecords(context);
        }

        private static void ImportCategoryProductsRecords(ProductShopContext context)
        {
            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            Random random = new Random();

            for (int i = 1; i <= 200; i++)
            {
                var categoryProduct = new CategoryProduct
                {
                    CategoryId = random.Next(1, 12),
                    ProductId = i
                };

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategoriesRecords(ProductShopContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            string xmlCategories = File.ReadAllText(@"..\..\..\XmlFiles\categories.xml");

            var categoriesDto = (CategoryDto[])serializer.Deserialize(new StringReader(xmlCategories));

            var categories = categoriesDto.Where(x => IsValid(x)).Select(x => Mapper.Map<Category>(x)).ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProductsRecords(ProductShopContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            string xmlProducts = File.ReadAllText(@"..\..\..\XmlFiles\products.xml");

            var productsDto = (ProductDto[])serializer.Deserialize(new StringReader(xmlProducts));

            var products = productsDto.Where(x => IsValid(x)).Select(x => Mapper.Map<Product>(x)).ToArray();
            Random random = new Random();

            foreach (var product in products)
            {
                product.SellerId = random.Next(1, 57);

                bool productWithoutBuyer = random.Next(1, 5) == 1;

                if (productWithoutBuyer)
                {
                    continue;
                }

                product.BuyerId = random.Next(1, 57);
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsersRecords(ProductShopContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            string xmlUsers = File.ReadAllText(@"..\..\..\XmlFiles\users.xml");

            var usersDto = (UserDto[])serializer.Deserialize(new StringReader(xmlUsers));

            var users = usersDto.Where(x => IsValid(x)).Select(x => Mapper.Map<User>(x)).ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}