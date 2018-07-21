using Employees.App.Contracts;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public abstract class Command : ICommand
    {
        protected IEmployeeControler employeeService;
        protected IManagerControler managerControler;

        public Command()
        {
        }

        protected Command(IEmployeeControler employeeService)
        {
            this.employeeService = employeeService;
        }

        public Command(IManagerControler managerControler)
        {
            this.managerControler = managerControler;
        }

    


        public abstract string Execute(List<string> arguments);
    }
}
