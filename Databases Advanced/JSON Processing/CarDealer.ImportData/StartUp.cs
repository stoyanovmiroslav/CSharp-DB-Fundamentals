using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer.ImportData
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            ImportSuppliersRecords(context);

            ImportPartsRecords(context);

            ImportCarsRecords(context);

            ImportPartCarsRecords(context);

            ImportCustomersRecords(context);

            ImportSalesRecords(context);
        }

        private static void ImportSalesRecords(CarDealerContext context)
        {
            List<Sale> sales = new List<Sale>();
            int[] discount = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };

            List<int> carIds = new List<int>();
            List<int> customerIds = new List<int>();

            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int discountIndex = random.Next(0, 8);
                int carId = random.Next(1, 359);
                int customerId = random.Next(1, 31);

                if (carIds.Contains(carId) && customerIds.Contains(customerId))
                {
                    continue;
                }

                customerIds.Add(customerId);
                carIds.Add(carId);

                Sale sale = new Sale { CarId = carId, CustomerId = customerId, Discount = discount[discountIndex] };

                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomersRecords(CarDealerContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/customers.json");

            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonString);

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportPartCarsRecords(CarDealerContext context)
        {
            List<PartCar> partCars = new List<PartCar>();
            Random random = new Random();

            for (int i = 1; i <= 358; i++)
            {
                int randomNumberOfParts = random.Next(10, 21);
                List<int> partIds = new List<int>();

                for (int j = 0; j < randomNumberOfParts; j++)
                {
                    int randomPartId = random.Next(1, 132);

                    if (partIds.Contains(randomPartId))
                    {
                        continue;
                    }

                    partIds.Add(randomPartId);

                    var part = new PartCar { CarId = i, PartId = randomPartId };
                    partCars.Add(part);
                }
            }
            context.PartCars.AddRange(partCars);
            context.SaveChanges();
        }

        private static void ImportCarsRecords(CarDealerContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/cars.json");

            var cars = JsonConvert.DeserializeObject<List<Car>>(jsonString);

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportPartsRecords(CarDealerContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/parts.json");

            var parts = JsonConvert.DeserializeObject<List<Part>>(jsonString);

            foreach (var supplier in parts)
            {
                supplier.SupplierId = new Random().Next(1, 32);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliersRecords(CarDealerContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/suppliers.json");

            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonString);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}