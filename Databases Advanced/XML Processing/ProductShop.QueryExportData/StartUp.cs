using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.QueryExportData.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.QueryExportData
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //Query 1. Products In Range
            ProductsInRange(context, xmlNamespaces);

            //Query 2. Sold Products
            SoldProducts(context, xmlNamespaces);

            //Query 3.Categories By Products Count
            CategoriesByProductsCount(context, xmlNamespaces);

            //Query 4. Users and Products
            UsersWithProducts(context, xmlNamespaces);

        }

        private static void UsersWithProducts(ProductShopContext context, XmlSerializerNamespaces xmlNamespaces)
        {
            var users = context.Users.Where(x => x.ProductsSold.Any(a => a.BuyerId != null))
                               .Select(x => new UP_UserDto
                               {
                                   FirstName = x.FirstName,
                                   LastName = x.LastName,
                                   Age = x.Age.ToString(),
                                   SoldProducts = new UP_SoldProductDto
                                   {
                                       Count = x.ProductsSold.Where(b => b.BuyerId != null).Count(),
                                       Product = x.ProductsSold.Where(b => b.BuyerId != null)
                                                    .Select(a => new UP_ProductDto
                                                    {
                                                        Name = a.Name,
                                                        Price = a.Price
                                                    }).ToArray()
                                   }
                               })
                               .OrderByDescending(x => x.SoldProducts.Count)
                               .ToArray();

            var usersProducts = new UP_UsersDto { Count = users.Count(), Users = users };

            XmlSerializer serializer = new XmlSerializer(typeof(UP_UsersDto));

            using (var writer = new StreamWriter("users-and-products.xml"))
            {
                serializer.Serialize(writer, usersProducts, xmlNamespaces);
            }
        }

        private static void CategoriesByProductsCount(ProductShopContext context, XmlSerializerNamespaces xmlNamespaces)
        {
            var categories = context.Categories
                                    .Select(x => new CP_CategoryDto
                                    {
                                        Name = x.Name,
                                        Count = x.CategoryProducts.Count,
                                        AveragePrice = x.CategoryProducts.Average(a => a.Product.Price),
                                        TotalRevenue = x.CategoryProducts.Sum(s => s.Product.Price)
                                    })
                                    .OrderByDescending(x => x.Count)
                                    .ToArray();


            XmlSerializer serializer = new XmlSerializer(typeof(CP_CategoryDto[]), new XmlRootAttribute("categories"));

            using (var writer = new StreamWriter("categories-by-products.xml"))
            {
                serializer.Serialize(writer, categories, xmlNamespaces);
            }
        }

        private static void SoldProducts(ProductShopContext context, XmlSerializerNamespaces xmlNamespaces)
        {
            var users = context.Users
                              .Where(x => x.ProductsBought.Count >= 1)
                              .Select(x => new SP_UserDto
                              {
                                  FirstName = x.FirstName,
                                  LastName = x.LastName,
                                  SoldProducts = x.ProductsBought.Select(p => new SP_ProductDto
                                  {
                                      Name = p.Name,
                                      Price = p.Price
                                  }).ToArray()
                              })
                              .OrderBy(x => x.LastName)
                              .ThenBy(x => x.FirstName)
                              .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(SP_UserDto[]), new XmlRootAttribute("users"));

            using (var writer = new StreamWriter("users-sold-products.xml"))
            {
                serializer.Serialize(writer, users, xmlNamespaces);
            }
        }

        private static void ProductsInRange(ProductShopContext context, XmlSerializerNamespaces xmlNamespaces)
        {
            var products = context.Products
                                  .Where(x => x.Price >= 1000 && x.Price <= 2000 && x.Buyer != null)
                                  .Select(x => new PR_SoldProducDto
                                  {
                                      Name = x.Name,
                                      Price = x.Price,
                                      Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
                                  })
                                  .OrderBy(x => x.Price)
                                  .ToArray();



            XmlSerializer serializer = new XmlSerializer(typeof(PR_SoldProducDto[]), new XmlRootAttribute("products"));

            using (var writer = new StreamWriter("products-in-range.xml"))
            {
                serializer.Serialize(writer, products, xmlNamespaces);
            }
        }
    }
}