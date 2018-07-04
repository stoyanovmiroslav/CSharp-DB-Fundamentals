using _02._Database_First.Data;
using System;
using System.Linq;

namespace _05._Employees_from_Research_and_Development
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            var employees = context.Employees
                                   .Select(e => new { e.FirstName, e.LastName, DepartmentName = e.Department.Name, e.Salary })
                                   .Where(e => e.DepartmentName == "Research and Development")
                                   .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName)
                                   .ToList();

            using (context)
            {
                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
                }
            }
        }
    }
}
