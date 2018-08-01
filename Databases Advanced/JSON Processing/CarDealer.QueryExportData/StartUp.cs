using CarDealer.Data;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CarDealer.QueryExportData
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new CarDealerContext();

            // Query 1.Ordered Customers
            OrderedCustomers(context);

            //Query 2. Cars from make Ferrari
            CarsFromMakeToyota(context);

            //Query 3. Local Suppliers
            LocalSuppliers(context);

            //Query 4. Cars with Their List of Parts
            CarsWithTheirListOfParts(context);

            //Query 5. Total Sales by Customer
            TotalSalesByCustomer(context);

            //Query 6. Sales with Applied Discount
            SalesWithAppliedDiscount(context);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales.Select(x => new
            {
                car = new
                {
                    Make = x.Car.Make,
                    Model = x.Car.Model,
                    TravelledDistance = x.Car.TravelledDistance
                },
                customerName = x.Customer.Name,
                Discount = x.Discount,
                price = x.Car.PartCars.Sum(p => p.Part.Price),
                priceWithDiscount = x.Car.PartCars.Sum(p => p.Part.Price) - (x.Car.PartCars.Sum(p => p.Part.Price) * x.Discount / 100)
            }).ToArray();

            var jsonString = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText("sales-discounts.json", jsonString);
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customer = context.Customers.Where(x => x.Sales.Count >= 1)

                                            .Select(x => new
                                            {
                                                fullName = x.Name,
                                                boughtCars = x.Sales.Count,
                                                spentMoney = x.Sales.Sum(a => a.Car.PartCars.Sum(p => p.Part.Price))
                                            })
                                            .OrderByDescending(x => x.spentMoney)
                                            .ThenByDescending(x => x.boughtCars)
                                            .ToArray();

            var jsonString = JsonConvert.SerializeObject(customer, Formatting.Indented);

            File.WriteAllText("customers-total-sales.json", jsonString);
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars.Select(x => new
            {
                car = new
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                },
                parts = x.PartCars.Select(p => new
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price
                }).ToArray()
            });

            var jsonString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("cars-and-parts.json", jsonString);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var supplier = context.Suppliers.Where(x => x.IsImporter == false)
                                  .Select(x => new
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      PartsCount = x.Parts.Count
                                  });

            var jsonString = JsonConvert.SerializeObject(supplier, Formatting.Indented);

            File.WriteAllText("local-suppliers.json", jsonString);
        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars.Where(x => x.Make == "Toyota")
                                   .Select(x => new
                                   {
                                       Id = x.Id,
                                       Make = x.Make,
                                       Model = x.Model,
                                       TravelledDistance = x.TravelledDistance
                                   })
                                   .OrderBy(x => x.Model)
                                   .ThenByDescending(x => x.TravelledDistance);

            var jsonString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("toyota-cars.json", jsonString);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver,
                Sales = x.Sales.Select(a => new
                {

                }).ToArray()
            })
            .OrderBy(x => x.BirthDate)
            .ThenBy(x => x.IsYoungDriver)
            .ToArray();

            var jsonString = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("ordered-customers.json", jsonString);
        }
    }
}