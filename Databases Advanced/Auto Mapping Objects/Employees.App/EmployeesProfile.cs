using AutoMapper;
using Employees.App.Models;
using Employees.ModelDto;
using Employees.Models;

namespace Employees.App
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();

            CreateMap<Employee, ManagerDto>().ForMember(dest => dest.EmployeesDto, from => from.MapFrom(x => x.Employees)).ReverseMap();

            CreateMap<Employee, EmployeeManagerDto>().ForMember(dest => dest.ManagerLastName, from => from.MapFrom(x => x.Manager.LastName)).ReverseMap();
        }
    }
}