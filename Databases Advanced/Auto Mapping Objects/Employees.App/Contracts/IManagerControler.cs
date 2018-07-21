using Employees.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Contracts
{
    public interface IManagerControler
    {
        void SetManager(int employeeId, int managerId);

        ManagerDto GetManagerInfo(int managerId);
    }
}
