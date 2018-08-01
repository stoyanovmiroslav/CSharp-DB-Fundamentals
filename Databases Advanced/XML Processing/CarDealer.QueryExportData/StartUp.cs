using CarDealer.Data;
using CarDealer.QueryExportData.Dtos;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace CarDealer.QueryExportData
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            var context = new CarDealerContext();

            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("", "");

            //Query 1. Cars with Distance
            CarsWithDistance(context, serializerNamespaces);

            //Query 2. Cars from make Ferrari
            CarsFromMakeFerrari(context, serializerNamespaces);

            //Query 3. Local Suppliers
            LocalSuppliers(context, serializerNamespaces);

            //Query 4. Cars with Their List of Parts
            CarsWithTheirListOfParts(context, serializerNamespaces);

            //Query 5. Total Sales by Customer
            TotalSalesByCustomer(context, serializerNamespaces);

            //Query 6. Sales with Applied Discount
            SalesWithAppliedDiscount(context, serializerNamespaces);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var sales = context.Sales
                   .Select(x => new SD_Sale
                   {
                       CustomerName = x.Customer.Name,
                       Price = x.Car.PartCars.Sum(p => p.Part.Price),
                       Discount = x.Discount,
                       TotalPrice = x.Car.PartCars.Sum(p => p.Part.Price) - (x.Car.PartCars.Sum(p => p.Part.Price) * x.Discount / 100.0m),
                       Car = new SD_CarDto
                       {
                           Make = x.Car.Make,
                           Model = x.Car.Model,
                           TravelledDistance = x.Car.TravelledDistance
                       }
                   }).ToArray();

            var serializer = new XmlSerializer(typeof(SD_Sale[]), new XmlRootAttribute("sales"));

            using (var writer = new StreamWriter("sales-discounts.xml"))
            {
                serializer.Serialize(writer, sales, serializerNamespaces);
            }
        }

        private static void TotalSalesByCustomer(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var customers = context.Customers.Where(x => x.Sales.Count >= 1)
                           .Select(x => new TC_CustomerDto
                           {
                               FullName = x.Name,
                               BoughtCars = x.Sales.Count,
                               SpentMoney = x.Sales.Sum(p => p.Car.PartCars.Sum(a => a.Part.Price))
                           }).ToArray();

            var serializer = new XmlSerializer(typeof(TC_CustomerDto[]), new XmlRootAttribute("customers"));

            using (var writer = new StreamWriter("cars-and-parts.xml"))
            {
                serializer.Serialize(writer, customers, serializerNamespaces);
            }
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var cars = context.Cars
                              .Select(x => new CP_CarDto
                              {
                                  Make = x.Make,
                                  Model = x.Model,
                                  TravelledDistance = x.TravelledDistance,
                                  Parts = x.PartCars.Select(p => new CP_PartDto
                                  {
                                      Name = p.Part.Name,
                                      Price = p.Part.Price
                                  }).ToArray()
                              }).ToArray();

            var serializer = new XmlSerializer(typeof(CP_CarDto[]), new XmlRootAttribute("cars"));

            using (var writer = new StreamWriter("cars-and-parts.xml"))
            {
                serializer.Serialize(writer, cars, serializerNamespaces);
            }
        }

        private static void LocalSuppliers(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var suppliers = context.Suppliers
                                  .Where(x => x.IsImporter == false)
                                  .Select(x => new LS_SupplierDto
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Count = x.Parts.Count
                                  }).ToArray();

            var serializer = new XmlSerializer(typeof(LS_SupplierDto[]), new XmlRootAttribute("suppliers"));

            using (var writer = new StreamWriter("local-suppliers.xml"))
            {
                serializer.Serialize(writer, suppliers, serializerNamespaces);
            }
        }

        private static void CarsFromMakeFerrari(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var cars = context.Cars.Where(x => x.Make == "Ferrari").Select(x => new CF_CarDto
            {
                Id = x.Id,
                Model = x.Model,
                TravelledDistance = x.TravelledDistance
            })
            .OrderBy(x => x.Model)
            .ThenByDescending(x => x.TravelledDistance)
            .ToArray();

            var serializer = new XmlSerializer(typeof(CF_CarDto[]), new XmlRootAttribute("cars"));

            using (var writer = new StreamWriter("ferrari-cars.xml"))
            {
                serializer.Serialize(writer, cars, serializerNamespaces);
            }
        }

        private static void CarsWithDistance(CarDealerContext context, XmlSerializerNamespaces serializerNamespaces)
        {
            var cars = context.Cars.Where(x => x.TravelledDistance > 2_000_000).Select(x => new CD_CarDto
            {
                Make = x.Make,
                Model = x.Model,
                TravelledDistance = x.TravelledDistance
            })
            .OrderBy(x => x.Make)
            .ThenBy(x => x.Model)
            .ToArray();

            var serializer = new XmlSerializer(typeof(CD_CarDto[]), new XmlRootAttribute("cars"));

            using (var writer = new StreamWriter("cars.xml"))
            {
                serializer.Serialize(writer, cars, serializerNamespaces);
            }
        }
    }
}