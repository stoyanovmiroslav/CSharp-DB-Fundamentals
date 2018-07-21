using System;
using System.Collections.Generic;
using System.Text;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.ModelDto;

namespace Employees.App.Commands
{
    public class ListEmployeesOlderThanCommand : Command
    {
        public ListEmployeesOlderThanCommand(IEmployeeControler employeeService) 
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            int age = int.Parse(arguments[0]);

            List<EmployeeManagerDto> employeesDto = this.employeeService.GetEmployeesOlderThan(age);

            StringBuilder sb = new StringBuilder();

            foreach (var e in employeesDto)
            {
                sb.Append($"{e.FirstName} {e.LastName} - ${e.Salary} - Manager: ");
                if (e.ManagerLastName == null)
                {
                    sb.AppendLine("[no manager]");
                }
                else
                {
                    sb.AppendLine($"{e.ManagerLastName}");
                }
            }

            if (employeesDto.Count == 0)
            {
                sb.AppendLine("There are no any employees!");
            }

            return sb.ToString().TrimEnd();
        }
    }
}