using _02._Database_First.Data;
using System;
using System.Linq;

namespace _15._Remove_Towns
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string town = Console.ReadLine();
            var context = new SoftUniContext();

            using (context)
            {
                var employees = context.Employees
                                       .Where(e => e.Address.Town.Name == town);
                foreach (var e in employees)
                {
                    e.AddressId = null;
                }

                var addresses = context.Addresses.Where(a => a.Town.Name == town);
                int numberOfAddresses = addresses.Count();
                context.Addresses.RemoveRange(addresses);

                var townObj = context.Towns.SingleOrDefault(t => t.Name == town);
                context.Towns.Remove(townObj);

                context.SaveChanges();

                Console.WriteLine($"{numberOfAddresses} address in {town} was deleted");
            }
        }
    }
}