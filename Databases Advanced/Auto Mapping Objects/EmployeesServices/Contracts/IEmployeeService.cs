using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services.Contracts
{
    public interface IEmployeeService
    {
        string AddEmployee(List<string> arguments);

        string SetBirthday(List<string> arguments);

        string SetAddress(List<string> arguments);

        string EmployeeInfo(List<string> arguments);

        string EmployeePersonalInfo(List<string> arguments);

        void Exit();

        string SetManager(List<string> arguments);

        string ManagerInfo(List<string> arguments);

        string ListEmployeesOlderThan(List<string> arguments);
    }
}