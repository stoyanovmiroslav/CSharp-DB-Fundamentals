using System;
using Employees.App.Contracts;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

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
            int employeeId = int.Parse(arguments[0]);
            string address = arguments[1];

            this.employeeService.SetAddress(employeeId, address);
            return "Address changed successfully!";
        }
    }
}
