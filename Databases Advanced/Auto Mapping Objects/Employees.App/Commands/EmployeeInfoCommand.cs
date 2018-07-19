using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class EmployeeInfoCommand : Command
    {
        public EmployeeInfoCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.EmployeeInfo(arguments);
            return result;
        }
    }
}
