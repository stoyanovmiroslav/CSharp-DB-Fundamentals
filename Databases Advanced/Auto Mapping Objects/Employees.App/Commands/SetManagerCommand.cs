using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    class SetManagerCommand : Command
    {
        public SetManagerCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.SetManager(arguments);
            return result;
        }
    }
}
