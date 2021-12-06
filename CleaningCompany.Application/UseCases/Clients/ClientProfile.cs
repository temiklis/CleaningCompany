using AutoMapper;
using CleaningCompany.Application.UseCases.Clients.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;

namespace CleaningCompany.Application.UseCases.Clients
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(c => c.FullName, s => s.MapFrom(c => $"{c.FirstName} {c.LastName}"))
                .ForMember(c => c.OrdersAmount, s => s.MapFrom(c => c.Orders.Count))
                .ForMember(c => c.Discount, s => s.MapFrom(c => ((Discount)c.Discount).ToString()));
        }
    }
}
