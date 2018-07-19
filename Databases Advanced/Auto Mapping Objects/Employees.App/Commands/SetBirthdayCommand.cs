using Employees.App.Contracts;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public class SetBirthdayCommand : Command
    {
        public SetBirthdayCommand(IEmployeeService employeeService)
            : base(employeeService)
        {
        }

        public override string Execute(List<string> arguments)
        {
            string result = this.employeeService.SetBirthday(arguments);
            return result;
        }
    }
}
