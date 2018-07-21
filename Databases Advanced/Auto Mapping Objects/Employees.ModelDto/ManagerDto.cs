using Employees.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.ModelDto
{
    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> EmployeesDto { get; set; }
    }
}
