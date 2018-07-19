using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class EmployeePersonalInfoCommand : Command
    {
        public EmployeePersonalInfoCommand(IEmployeeService employeeService)
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.EmployeePersonalInfo(arguments);
            return result;
        }
    }
}