using _02._Database_First.Data;
using System;
using System.Linq;

namespace _10._Departments_with_More_Than_5_Employees
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            using (context)
            {
                var departments = context.Departments
                                         .Select(d => new
                                         {
                                             departmentName = d.Name,
                                             fullNameManager = d.Manager.FirstName + " " + d.Manager.LastName,
                                             employees = d.Employees.Select(e => new { e.FirstName, e.LastName, e.JobTitle }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName)})
                                         .Where(x => x.employees.Count() > 5)
                                         .OrderBy(x => x.employees.Count())
                                         .ThenBy(x => x.departmentName);

                foreach (var d in departments)
                {
                    Console.WriteLine($"{d.departmentName} - {d.fullNameManager}");
                    foreach (var e in d.employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                    }

                    Console.WriteLine("----------");
                }
            }
        }
    }
}