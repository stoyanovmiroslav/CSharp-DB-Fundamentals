using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.Models;
using Employees.Services;
using Employees.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.App
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var dbInitializerService = serviceProvider.GetService<IDbInitializerService>();
            dbInitializerService.Initializer();
            dbInitializerService.Seed();

            var commandInterpreter = serviceProvider.GetService<ICommandInterpreter>();
            
            
            string input = "";

            while ((input = Console.ReadLine()) != "Stop")
            {
                try
                {
                    List<string> commandParameters = input.Split(" ").ToList();
                    string commandName = commandParameters[0];

                    ICommand classInstance = commandInterpreter.InterpretCommand(commandName);
                    Console.WriteLine(classInstance.Execute(commandParameters.Skip(1).ToList()));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}