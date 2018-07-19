using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            this.employeeService.Exit();
            return string.Empty;
        }
    }
}
