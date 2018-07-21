using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class ExitCommand : Command
    {
        public override string Execute(List<string> arguments)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}