using AutoMapper;
using CarDealer.Data;
using CarDealer.ImportData.Dtos;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
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
            context.Database.Migrate();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Supplier, SupplierDto>().ReverseMap();
                cfg.CreateMap<Part, PartDto>().ReverseMap();
                cfg.CreateMap<Car, CarDto>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDto>().ReverseMap();
            });

            ImportSuppliersRecords(context);

            ImportPartsRecords(context);

            ImportCarsRecords(context);

            ImportPartCarsRecords(context);

            ImportCustomersRecords(context);

            ImportSalesRecords(context);
        }

        private static void ImportCustomersRecords(CarDealerContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));

            string xmlcustomers = File.ReadAllText(@"..\..\..\XmlFiles\customers.xml");

            var customerDto = (CustomerDto[])serializer.Deserialize(new StringReader(xmlcustomers));

            var customers = customerDto.Select(x => Mapper.Map<Customer>(x)).ToArray();
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
            XmlSerializer serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));

            string xmlcars = File.ReadAllText(@"..\..\..\XmlFiles\cars.xml");

            var carsDto = (CarDto[])serializer.Deserialize(new StringReader(xmlcars));

            List<Car> cars = new List<Car>();

            for (int i = 0; i < carsDto.Length; i++)
            {
                var car = Mapper.Map<Car>(carsDto[i]);


                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportPartsRecords(CarDealerContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));

            string xmlparts = File.ReadAllText(@"..\..\..\XmlFiles\parts.xml");

            var partsDto = (PartDto[])serializer.Deserialize(new StringReader(xmlparts));

            Random random = new Random();
            List<Part> parts = new List<Part>();

            for (int i = 0; i < partsDto.Length; i++)
            {
                var part = Mapper.Map<Part>(partsDto[i]);
                part.SupplierId = random.Next(1, 32);

                parts.Add(part);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliersRecords(CarDealerContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));

            string xmlSuppliers = File.ReadAllText(@"..\..\..\XmlFiles\suppliers.xml");

            var suppliersDto = (SupplierDto[])serializer.Deserialize(new StringReader(xmlSuppliers));

            var suppliers = suppliersDto.Select(x => Mapper.Map<Supplier>(x));

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
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
    }
}