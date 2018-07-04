using _02._Database_First.Data;
using _02._Database_First.Data.Models;
using System;
using System.Linq;

namespace _06._Adding_Address_and_Updating_Employee
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

           

            using (context)
            {
                var newAddress = new Address { AddressText = "Vitoshka 15", TownId = 4 };
                context.Addresses.Add(newAddress);

                Employee employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
                employee.Address = newAddress;

                context.SaveChanges();

                var employees = context.Employees
                                       .Select(e => new { e.AddressId, e.Address.AddressText })
                                       .OrderByDescending(e => e.AddressId)
                                       .Take(10);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.AddressText}");
                }
            }
        }
    }
}
