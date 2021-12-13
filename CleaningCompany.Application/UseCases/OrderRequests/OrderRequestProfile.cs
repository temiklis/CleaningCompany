using AutoMapper;
using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using CleaningCompany.Domain.Entities;
using System.Linq;

namespace CleaningCompany.Application.UseCases.OrderRequests
{
    public class OrderRequestProfile : Profile
    {
        public OrderRequestProfile()
        {
            CreateMap<CreateOrderRequestCommand, OrderRequest>()
                .ForMember(o => o.Products, s => s.Ignore());

            CreateMap<OrderRequest, OrderRequestDto>()
                .ForMember(o => o.UserAddress, s => s.MapFrom(p => p.Address))
                .ForMember(o => o.UserEmail, s => s.MapFrom(p => p.Email))
                .ForMember(o => o.UserFIO, s => s.MapFrom(p => p.FIO))
                .ForMember(o => o.Products, s => s.MapFrom(or => string.Join(',', or.Products.Select(p => p.Name))));

            CreateMap<OrderRequest, UserOrderRequestDto>()
                .ForMember(o => o.Products, s => s.MapFrom(or => string.Join(',', or.Products.Select(p => p.Name))));

            CreateMap<OrderRequest, OrderRequestDetailsDto>()
                .ForMember(o => o.UserAddress, s => s.MapFrom(p => p.Address))
                .ForMember(o => o.UserEmail, s => s.MapFrom(p => p.Email))
                .ForMember(o => o.UserFIO, s => s.MapFrom(p => p.FIO))
                .ForMember(o => o.Products, s => s.MapFrom(or => or.Products.Select(p => p.Name)));
        }
    }
}
