using System;
using Employees.App.Contracts;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public class SetAddressCommand : Command
    {
        public SetAddressCommand(IEmployeeControler employeeService) 
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
