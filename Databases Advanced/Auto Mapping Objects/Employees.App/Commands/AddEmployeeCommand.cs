using AutoMapper;
using Employees.App.Contracts;
using Employees.App.Models;
using Employees.Models;
using Employees.Services;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    public class AddEmployeeCommand : Command
    {
        public AddEmployeeCommand(IEmployeeControler employeeService) 
            : base(employeeService)
        {
        }
        
        public override string Execute(List<string> arguments)
        {
            string firstName = arguments[0];
            string lastName = arguments[1];
            decimal salary = decimal.Parse(arguments[2]);

            EmployeeDto employeeDto = new EmployeeDto() { FirstName = firstName, LastName = lastName, Salary = salary };

            this.employeeService.AddEmployee(employeeDto);

            return "Employee added successfully!";
        }
    }
}