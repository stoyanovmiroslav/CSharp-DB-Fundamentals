using Employees.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Employees.App
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand InterpretCommand(string commandName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var currentCommand = assembly.GetTypes().FirstOrDefault(c => c.Name.ToLower() == commandName.ToLower() + "command");

            var constructor = currentCommand.GetConstructors().First();
            var parameters = constructor.GetParameters().Select(x => x.ParameterType).ToList();

            var services = parameters.Select(serviceProvider.GetService).ToArray();

            object[] ctorArgs = new object[] { services };

           return (ICommand)constructor.Invoke(services);
        }
    }
}