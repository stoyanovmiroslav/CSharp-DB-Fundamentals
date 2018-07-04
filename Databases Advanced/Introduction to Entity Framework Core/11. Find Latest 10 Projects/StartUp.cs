using _02._Database_First.Data;
using System;
using System.Globalization;
using System.Linq;

namespace _11._Find_Latest_10_Projects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            var projects = context.Projects
                                  .Select(x => new
                                  {
                                      x.Name,
                                      x.Description,
                                      x.StartDate
                                  })
                                  .OrderByDescending(x => x.StartDate)
                                  .Take(10);

            foreach (var p in projects.OrderBy(x => x.Name))
            {
                Console.WriteLine($"{p.Name}");
                Console.WriteLine($"{p.Description}");
                Console.WriteLine($"{p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
            }
        }
    }
}
