using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Contracts
{
    public interface ICommandInterpreter
    {
        ICommand InterpretCommand(string commandName);
    }
}
