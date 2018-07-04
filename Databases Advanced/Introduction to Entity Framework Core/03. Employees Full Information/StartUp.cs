using _02._Database_First.Data;
using System;
using System.Linq;

namespace _03._Employees_Full_Information
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            using (context)
            {
                var employees = context.Employees
                                       .Select(e => new { e.EmployeeId, e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary })
                                       .OrderBy(x => x.EmployeeId);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
                }
            }
        }
    }
}