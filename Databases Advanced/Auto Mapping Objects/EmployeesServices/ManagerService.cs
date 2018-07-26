using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employees.Data;
using Employees.ModelDto;
using Employees.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.Services
{
    public class ManagerService : IManagerService
    {
        EmployeesContext context;

        public ManagerService(EmployeesContext context)
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