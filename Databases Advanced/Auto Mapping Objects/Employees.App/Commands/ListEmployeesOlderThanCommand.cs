using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class ListEmployeesOlderThanCommand : Command
    {
        public ListEmployeesOlderThanCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.ListEmployeesOlderThan(arguments);

            return result;
        }
    }
}