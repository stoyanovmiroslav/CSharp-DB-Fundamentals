using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public class SetAddressCommand : Command
    {
        public SetAddressCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.SetAddress(arguments);
            return result;
        }
    }
}
