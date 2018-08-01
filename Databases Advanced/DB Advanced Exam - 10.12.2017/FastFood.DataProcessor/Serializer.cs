using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
            Enum.TryParse<OrderType>(orderType, out OrderType type);

            var orders = new
            {
                Name = employeeName,
                Orders = context.Orders.Where(x => x.Employee.Name == employeeName && x.Type == type)
                                       .OrderByDescending(x => x.TotalPrice)
                                       .ThenByDescending(x => x.OrderItems.Count)
                                       .Select(x => new
                                           {
                                               Customer = x.Customer,
                                               Items = x.OrderItems.Select(o => new
                                               {
                                                   Name = o.Item.Name,
                                                   Price = o.Item.Price,
                                                   Quantity = o.Quantity

                                               }).ToArray(),

                                               TotalPrice = x.TotalPrice
                                       }).ToArray(),

                                      TotalMade = context.Orders.Where(x => x.Employee.Name == employeeName && x.Type == type).Sum(x => x.TotalPrice)
            };

            var jsonString = JsonConvert.SerializeObject(orders, Formatting.Indented);

            return jsonString;
        }

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categoriesNames = categoriesString.Split(",");

            var categories = context.Categories.Where(x => categoriesNames.Contains(x.Name))//.ToArray();
                                               .Select(x => new CategoryDto
                                               {
                                                   Name = x.Name,
                                                   MostPopularItem = x.Items.Select(i => new MostPopularItemDto
                                                   {
                                                       Name = i.Name,
                                                       TimesSold = i.OrderItems.Sum(s => s.Quantity),
                                                       TotalMade = i.OrderItems.Sum(s => s.Quantity) * i.Price
                                                   }).OrderByDescending(a => a.TotalMade).FirstOrDefault()

                                                }).OrderByDescending(x => x.MostPopularItem.TotalMade).ToArray();

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            StringBuilder sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

            return sb.ToString().TrimEnd();
		}
	}
}