using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            var employeesDto = JsonConvert.DeserializeObject<List<EmployeeDto>>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Employee> employees = new List<Employee>();
            List<Position> positions = new List<Position>();

            foreach (var e in employeesDto)
            {
                if (!IsValid(e))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                var position = context.Positions.FirstOrDefault(x => x.Name == e.Position);

                if (position == null)
                {
                    if (positions.Any(x => x.Name == e.Position))
                    {
                        position = positions.FirstOrDefault(x => x.Name == e.Position);
                    }
                    else
                    {
                        position = new Position { Name = e.Position };
                        positions.Add(position);
                    }
                }

                var employee = new Employee { Name = e.Name, Age = e.Age, Position = position };

                employees.Add(employee);
                sb.AppendLine($"Record {e.Name} successfully imported.");
            }

            context.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var itemsDto = JsonConvert.DeserializeObject<List<ItemDto>>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Category> categories = new List<Category>();
            List<Item> items = new List<Item>();

            foreach (var itemDto in itemsDto)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var item = context.Items.FirstOrDefault(x => x.Name == itemDto.Name);

                if (item != null || items.Any(x => x.Name == itemDto.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var category = context.Categories.FirstOrDefault(x => x.Name == itemDto.Category);

                if (category == null)
                {
                    if (categories.Any(x => x.Name == itemDto.Category))
                    {
                        category = categories.FirstOrDefault(x => x.Name == itemDto.Category);
                    }
                    else
                    {
                        category = new Category { Name = itemDto.Category };
                        categories.Add(category);
                    }
                }

                item = new Item { Name = itemDto.Name, Price = itemDto.Price, Category = category };
                items.Add(item);
                sb.AppendLine($"Record {itemDto.Name} successfully imported.");
            }

            context.Items.AddRange(items);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));

            var ordersDto = (OrderDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            List<Order> orders = new List<Order>();

            foreach (var orderDto in ordersDto)
            {
                if (!IsValid(orderDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                bool isDateTimeCorrect = DateTime.TryParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);

                var employee = context.Employees.FirstOrDefault(x => x.Name == orderDto.Employee);

                var isTypeValid = Enum.TryParse<OrderType>(orderDto.Type, out OrderType type);
     
                if (!isDateTimeCorrect || !isTypeValid || employee == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var order = new Order { Customer = orderDto.Customer, DateTime = dateTime, Employee = employee, Type = type };

                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var item in orderDto.Items)
                {
                    int? itemId = context.Items.FirstOrDefault(x => x.Name == item.Name)?.Id;

                    if (itemId == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    var orderItem = new OrderItem { ItemId = itemId.Value, Order = order, Quantity = int.Parse(item.Quantity) };

                    orderItems.Add(orderItem);
                }

                order.OrderItems = orderItems;

                orders.Add(order);
                sb.AppendLine($"Order for {order.Customer} on {dateTime.ToString("dd/MM/yyyy HH:mm")} added");
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}