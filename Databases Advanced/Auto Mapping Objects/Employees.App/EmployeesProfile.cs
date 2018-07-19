using AutoMapper;
using Employees.App.Models;
using Employees.Models;

namespace Employees.App
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}