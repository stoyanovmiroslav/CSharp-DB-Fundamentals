using _02._Database_First.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _12._Increase_Salaries
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] departments = { "Engineering", "Tool Design", "Marketing", "Information Services" };
            var context = new SoftUniContext();

            using (context)
            {
                var employees = context.Employees
                                 .Include(e => e.Department)
                                 .Where(e => departments.Contains(e.Department.Name))
                                 .OrderBy(x => x.FirstName)
                                 .ThenBy(x => x.LastName);

                foreach (var e in employees)
                {
                    e.Salary *= 1.12m;
                    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                }

                context.SaveChanges();
            }
        }
    }
}