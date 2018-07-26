using Employees.App.Contracts;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            int employeeId = int.Parse(arguments[0]);
            DateTime birthday = DateTime.ParseExact(arguments[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            this.employeeService.SetBirthday(employeeId, birthday);
            return "Birthday changed successfully!";
        }
    }
}
