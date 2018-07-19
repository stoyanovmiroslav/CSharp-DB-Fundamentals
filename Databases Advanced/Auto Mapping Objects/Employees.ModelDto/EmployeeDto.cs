using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Salary { get; set; }

        public EmployeeDto Manager { get; set; }
    }
}