using AutoMapper;
using Employees.App.Contracts;
using Employees.Data;
using Employees.Models;
using Employees.Services;
using Employees.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Employees.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //using (var context = new EmployeesContext())
            //{
            //    context.Database.EnsureDeleted();
            //    context.Database.Migrate();
            //    Seed(context);
            //}

            var serviceProvider = ConfigureServices();

            ICommandInterpreter commandInterpreter = new CommandInterpreter(serviceProvider);

            Engine engine = new Engine(serviceProvider, commandInterpreter);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(option => option.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<EmployeesProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        private static void Seed(EmployeesContext context)
        {
            var employees = new Employee[]
            {
                new Employee { FirstName = "Ivailo", LastName = "Ivanov", Salary = 1000, Address = "Sofia" },
                new Employee { FirstName = "Georgi", LastName = "Georgiev", Salary = 1500, Address = "Stara Zagora" },
                new Employee { FirstName = "Michael", LastName = "Atanasov", Salary = 1300, Address = "Burgas" },
                new Employee { FirstName = "Stanko", LastName = "Stoyanov", Salary = 1660, Address = "Vidin" },
                new Employee { FirstName = "Mariya", LastName = "Ivanova", Salary = 1000, Address = "Varna" }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
