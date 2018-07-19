using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.Data;
using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.App
{
    public class Engine
    {
        private IServiceProvider serviceProvider;
        private ICommandInterpreter commandInterpreter;

        public Engine(IServiceProvider serviceProvider, ICommandInterpreter commandInterpreter)
        {
            this.serviceProvider = serviceProvider;
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
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