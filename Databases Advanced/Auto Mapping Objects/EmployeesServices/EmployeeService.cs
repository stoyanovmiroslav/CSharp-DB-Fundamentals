using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employees.App.Models;
using Employees.Data;
using Employees.ModelDto;
using Employees.Models;
using Employees.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        EmployeesContext context;
        
        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = Mapper.Map<EmployeeDto, Employee>(employeeDto);

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employeeInfo = context.Employees.Find(employeeId);
            EmployeeDto employeeInfoDto = Mapper.Map<Employee, EmployeeDto>(employeeInfo);

            return employeeInfoDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employeeInfo = context.Employees.Find(employeeId);
            EmployeePersonalInfoDto employeeInfoDto = Mapper.Map<Employee, EmployeePersonalInfoDto>(employeeInfo);

            return employeeInfoDto;
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            employee.Address = address;
            context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime birthday)
        {
            var employee = context.Employees.Find(employeeId);

            employee.Birthday = birthday;
            context.SaveChanges();
        }

        public List<EmployeeManagerDto> GetEmployeesOlderThan(int age)
        {
            var employeeDto = context.Employees
                                .Where(e => e.Birthday != null && e.Birthday.Value.Year + age < DateTime.Now.Year)
                                .ProjectTo<EmployeeManagerDto>().ToList();
            
            return employeeDto;
        }
    }
}