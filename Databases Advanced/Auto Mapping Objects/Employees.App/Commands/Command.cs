using Employees.App.Contracts;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public abstract class Command : ICommand
    {
        protected IEmployeeService employeeService;
        protected IManagerService managerControler;

        public Command()
        {
        }

        protected Command(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public Command(IManagerService managerControler)
        {
            this.managerControler = managerControler;
        }

    


        public abstract string Execute(List<string> arguments);
    }
}
