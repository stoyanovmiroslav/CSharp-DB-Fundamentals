using _02._Database_First.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace _07._Employees_and_Projects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            using (context)
            {
                var employees = context.Employees
                                       .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >=2001  && p.Project.StartDate.Year <= 2003))
                                       .Select(e => new
                                      {
                                          e.FirstName,
                                          e.LastName,
                                          ManagerFirstName = e.Manager.FirstName,
                                          ManagerLastName = e.Manager.LastName,
                                          projects = e.EmployeesProjects.Select(p => new { p.Project.Name, p.Project.StartDate, p.Project.EndDate })
                                      }).Take(30);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} – Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                    string format = "M/d/yyyy h:mm:ss tt";

                    foreach (var p in e.projects)
                    {
                        Console.WriteLine($"--{p.Name} - {p.StartDate.ToString(format, CultureInfo.InvariantCulture)} - {(p.EndDate?.ToString(format, CultureInfo.InvariantCulture) ?? "not finished")}");
                    }
                }
            }
        }
    }
}
