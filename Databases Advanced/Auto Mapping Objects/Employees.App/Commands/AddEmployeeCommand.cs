using Employees.App.Contracts;
using Employees.Services;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public class AddEmployeeCommand : Command
    {
        public AddEmployeeCommand(IEmployeeService employeeService) 
            : base(employeeService)
        {
        }
        
        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.AddEmployee(arguments);
            return result;
        }
    }
}