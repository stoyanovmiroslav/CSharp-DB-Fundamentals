using System.Collections.Generic;

namespace Employees.App.Contracts
{
    public interface ICommand
    {
        string Execute(List<string> arguments);
    }
}