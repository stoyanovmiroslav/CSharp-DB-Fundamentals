using System;
using System.Collections.Generic;
using System.Text;
using Employees.App.Contracts;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    class SetManagerCommand : Command
    {
        public SetManagerCommand(IManagerService managerControler) 
            : base(managerControler)
        {
        }

        public override string Execute(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            int managerId = int.Parse(arguments[1]);

            this.managerControler.SetManager(employeeId, managerId);
            return "Manager changed successfully!";
        }
    }
}
