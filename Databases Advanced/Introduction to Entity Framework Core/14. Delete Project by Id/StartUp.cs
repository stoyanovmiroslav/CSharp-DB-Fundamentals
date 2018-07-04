using _02._Database_First.Data;
using System;
using System.Linq;

namespace _14._Delete_Project_by_Id
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();

            using (context)
            {
                var project = context.Projects
                                     .SingleOrDefault(p => p.ProjectId == 2);

                var employeesProjects = context.EmployeesProjects
                                               .Where(p => p.ProjectId == 2);

                context.EmployeesProjects.RemoveRange(employeesProjects);
                context.Projects.Remove(project);

                context.SaveChanges();

                var projects = context.Projects
                                      .Select(p => new { p.Name })
                                      .Take(10);

                foreach (var p in projects)
                {
                    Console.WriteLine(p.Name);
                }
            }
        }
    }
}