using Employees.App.Models;
using Employees.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Contracts
{
    public interface IEmployeeControler
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime birthday);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);

        List<EmployeeManagerDto> GetEmployeesOlderThan(int age);
    }
}