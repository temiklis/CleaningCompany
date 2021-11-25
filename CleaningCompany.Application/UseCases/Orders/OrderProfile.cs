using AutoMapper;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;
using System.Linq;

namespace CleaningCompany.Application.UseCases.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Employees, s => s.MapFrom(o => string.Join(',', o.ResponsibleEmployees.Select(e => $"{e.FirstName} {e.LastName}"))))
                .ForMember(o => o.Products, s => s.MapFrom(o => string.Join(',', o.Products.Select(p => p.Name))))
                .ForMember(o => o.ClientEmail, s => s.MapFrom(o => o.Client.Email))
                .ForMember(o => o.Status, s => s.MapFrom(o => ((Status)o.Status).ToString()));
        }
    }
}
