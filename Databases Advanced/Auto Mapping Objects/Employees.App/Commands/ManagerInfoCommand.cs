using System;
using System.Collections.Generic;
using System.Text;
using Employees.ModelDto;
using Employees.App.Contracts;

namespace Employees.App.Commands
{
    public class ManagerInfoCommand : Command
    {
        public ManagerInfoCommand(IManagerControler managerControler)
           : base(managerControler)
        {
        }

        public override string Execute(List<string> arguments)
        {
            int managerId = int.Parse(arguments[0]);

            ManagerDto manager = this.managerControler.GetManagerInfo(managerId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.EmployeesDto.Count}");
            foreach (var e in manager.EmployeesDto)
            {
                sb.AppendLine($"    - {e.FirstName} {e.LastName} - {e.Salary}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
