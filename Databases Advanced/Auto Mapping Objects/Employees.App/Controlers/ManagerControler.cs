using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employees.App.Contracts;
using Employees.Data;
using Employees.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.App.Controlers
{
    public class ManagerControler : IManagerControler
    {
        EmployeesContext context;

        public ManagerControler(EmployeesContext context)
        {
            this.context = context;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = context.Employees.Find(employeeId);
            employee.ManagerId = managerId;

            context.SaveChanges();
        }

        public ManagerDto GetManagerInfo(int managerId)
        {
            var managerDto = context.Employees
                             .Where(e => e.Id == managerId)
                             .ProjectTo<ManagerDto>().SingleOrDefault();

            return managerDto;
        }
    }
}