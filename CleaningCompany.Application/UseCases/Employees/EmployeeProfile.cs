using AutoMapper;
using CleaningCompany.Application.UseCases.Employees.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(e => e.FullName, s => s.MapFrom(e => $"{e.FirstName} {e.LastName}"));
        }
    }
}
