using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.ModelDto
{
    public class EmployeeManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerLastName { get; set; }
    } 
}
