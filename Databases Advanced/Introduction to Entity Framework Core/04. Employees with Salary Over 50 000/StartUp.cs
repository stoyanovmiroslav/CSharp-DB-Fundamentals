using _02._Database_First.Data;
using System;
using System.Linq;

namespace _04._Employees_with_Salary_Over_50_000
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            using (context)
            {
                var employees = context.Employees
                                       .Select(e => new { e.FirstName, e.Salary })
                                       .Where(x => x.Salary > 50000).OrderBy(x => x.FirstName);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName}");
                }
            }
        }
    }
}
