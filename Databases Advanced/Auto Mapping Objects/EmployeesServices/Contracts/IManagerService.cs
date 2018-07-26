using Employees.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services.Contracts
{
    public interface IManagerService
    {
        void SetManager(int employeeId, int managerId);

        ManagerDto GetManagerInfo(int managerId);
    }
}
