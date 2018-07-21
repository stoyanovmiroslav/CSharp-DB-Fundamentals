using AutoMapper;
using Employees.App.Contracts;
using Employees.App.Controlers;
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
            var serviceProvider = ConfigureServices();

            Engine engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(option => option.UseLazyLoadingProxies().UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IEmployeeControler, EmployeeControler>();

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<IManagerControler, ManagerControler>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<EmployeesProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
