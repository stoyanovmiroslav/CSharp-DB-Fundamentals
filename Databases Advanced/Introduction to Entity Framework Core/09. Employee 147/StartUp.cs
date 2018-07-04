using _02._Database_First.Data;
using System;
using System.Linq;

namespace _09._Employee_147
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            using (context)
            {
                var employee = context.Employees
                                      .Select(e => new
                                      {
                                          e.FirstName,
                                          e.LastName,
                                          e.JobTitle,
                                          e.EmployeeId,
                                          projects = e.EmployeesProjects.Select(p => new { p.Project.Name }).OrderBy(p => p.Name)
                                      })
                                      .SingleOrDefault(e => e.EmployeeId == 147);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                foreach (var p in employee.projects)
                {
                    Console.WriteLine($"{p.Name}");
                }
            }
        }
    }
}
