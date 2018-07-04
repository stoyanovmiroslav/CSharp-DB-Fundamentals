using _02._Database_First.Data;
using System;
using System.Linq;

namespace _13._Find_Employees_by_Name_Starting_With_Sa
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var context = new SoftUniContext();

            using (context)
            {
                var employees = context.Employees
                                       .Select(e => new
                                       {
                                           e.FirstName,
                                           e.LastName,
                                           e.JobTitle,
                                           e.Salary
                                       })
                                       .Where(e => e.FirstName.StartsWith("Sa"))
                                       .OrderBy(e => e.FirstName)
                                       .ThenBy(e => e.LastName);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                }
            }
        }
    }
}
