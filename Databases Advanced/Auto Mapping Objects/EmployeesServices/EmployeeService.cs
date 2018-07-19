using Employees.Data;
using Employees.Models;
using Employees.Services.Contracts;
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

        public object StringBilder { get; private set; }

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public string AddEmployee(List<string> arguments)
        {
            string firstName = arguments[0];
            string lastName = arguments[1];
            decimal salary = decimal.Parse(arguments[2]);

            var employee = new Employee() { FirstName = firstName, LastName = lastName, Salary = salary };

            context.Employees.Add(employee);
            context.SaveChanges();

            return "Employee added successfully!";
        }

        public string EmployeeInfo(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            var employeeInfo = context.Employees.Find(employeeId);

            return $"ID: {employeeInfo.Id} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:f2}";
        }

        public string EmployeePersonalInfo(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            var employeeInfo = context.Employees.Find(employeeId);

            string result = $"ID: {employeeInfo.Id} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:f2}" +
                 Environment.NewLine + $"Birthday: {employeeInfo.Birthday?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}" +
                 Environment.NewLine + $"Address: {employeeInfo.Address}";

            return result;
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public string SetAddress(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            string address = arguments[1];

            var employee = context.Employees.Find(employeeId);

            employee.Address = address;
            context.SaveChanges();

            return "Address changed successfully!";
        }

        public string SetBirthday(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            DateTime birthday = DateTime.ParseExact(arguments[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var employee = context.Employees.Find(employeeId);

            employee.Birthday = birthday;
            context.SaveChanges();

            return "Birthday changed successfully!";
        }

        public string SetManager(List<string> arguments)
        {
            int employeeId = int.Parse(arguments[0]);
            int managerId = int.Parse(arguments[1]);

            var employee = context.Employees.Find(employeeId);
            employee.ManagerId = managerId;
            context.SaveChanges();

            return "Manager changed successfully!";
        }

        public string ManagerInfo(List<string> arguments)
        {
            int managerId = int.Parse(arguments[0]);

            var manager = context.Employees.Find(managerId);

            var employees = context.Employees
                                   .Where(e => e.ManagerId == managerId)
                                   .Select(e => new
                                    {
                                        FullName = e.FirstName + " " + e.LastName,
                                        e.Salary
                                    }).ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {employees.Count()}");
            foreach (var e in employees)
            {
                sb.AppendLine($"    - {e.FullName} - {e.Salary}");
            }

            return sb.ToString().TrimEnd();
        }

        public string ListEmployeesOlderThan(List<string> arguments)
        {
            int age = int.Parse(arguments[0]);

            var employees = context.Employees
                                   .Where(e => e.Birthday != null)
                                   .Where(e => (DateTime.Now - e.Birthday).Value.TotalDays / 365.242199 > age)
                                   .Select(e => new
                                   {
                                       FullName = e.FirstName + " " + e.LastName,
                                       e.Manager,
                                       e.Salary
                                   }).ToList();

            StringBuilder sb = new StringBuilder();
            
            foreach (var e in employees)
            {
                sb.Append($"{e.FullName} - ${e.Salary} - Manager: ");
                if (e.Manager == null)
                {
                    sb.AppendLine("[no manager]");
                }
                else
                {
                    sb.AppendLine($"{e.Manager.LastName}");
                }
            }

            if (employees.Count == 0)
            {
                sb.AppendLine("There are no any employees!");
            }

            return sb.ToString().TrimEnd();
        }
    }
}