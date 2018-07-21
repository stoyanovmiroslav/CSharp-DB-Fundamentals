using System;
using System.Collections.Generic;
using System.Text;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class EmployeeInfoCommand : Command
    {
        public EmployeeInfoCommand(IEmployeeControler employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);

            EmployeeDto employeeInfo = this.employeeService.GetEmployeeInfo(employeeId);

            return $"ID: {employeeInfo.Id} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:f2}";;
        }
    }
}
