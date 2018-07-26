using Employees.App.Models;
using Employees.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services.Contracts
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime birthday);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);

        List<EmployeeManagerDto> GetEmployeesOlderThan(int age);
    }
}