using _02._Database_First.Data;
using System;
using System.Linq;

namespace _08._Addresses_by_Town
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            using (context)
            {
                var addresses = context.Addresses
                                       .Select(a => new
                                       {
                                           a.AddressText,
                                           a.Town.Name,
                                           a.Employees.Count
                                       })
                                       .OrderByDescending(x => x.Count)
                                       .ThenBy(x => x.Name)
                                       .Take(10);

                foreach (var a in addresses)
                {
                    Console.WriteLine($"{a.AddressText}, {a.Name} - {a.Count} employees");
                }
            }
        }
    }
}