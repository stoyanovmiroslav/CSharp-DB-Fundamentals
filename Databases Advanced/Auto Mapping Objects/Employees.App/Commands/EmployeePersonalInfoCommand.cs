using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.ModelDto;


namespace Employees.App.Commands
{
    public class EmployeePersonalInfoCommand : Command
    {
        public EmployeePersonalInfoCommand(IEmployeeControler employeeService)
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);

            EmployeePersonalInfoDto employeeInfo = this.employeeService.GetEmployeePersonalInfo(employeeId);

            string result = $"ID: {employeeInfo.Id} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:f2}" +
             Environment.NewLine + $"Birthday: {employeeInfo.Birthday?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}" +
             Environment.NewLine + $"Address: {employeeInfo.Address}";
            return result;
        }
    }
}