using Employees.Data;
using Employees.Models;
using Employees.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly EmployeesContext employeesContext;

        public DbInitializerService(EmployeesContext employeesContex)
        {
            this.employeesContext = employeesContex;
        }

        public void Initializer()
        {
            this.employeesContext.Database.Migrate();
        }

        public void Seed()
        {
            var employees = new Employee[]
            {
                new Employee { FirstName = "Ivailo", LastName = "Ivanov", Salary = 1000, Address = "Sofia" },
                new Employee { FirstName = "Georgi", LastName = "Georgiev", Salary = 1500, Address = "Stara Zagora" },
                new Employee { FirstName = "Michael", LastName = "Atanasov", Salary = 1300, Address = "Burgas" },
                new Employee { FirstName = "Stanko", LastName = "Stoyanov", Salary = 1660, Address = "Vidin" },
                new Employee { FirstName = "Mariya", LastName = "Ivanova", Salary = 1000, Address = "Varna" }
            };

            this.employeesContext.Employees.AddRange(employees);
            this.employeesContext.SaveChanges();
        }
    }
}