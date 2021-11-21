using AutoMapper;
using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.OrderRequests
{
    public class OrderRequestProfile : Profile
    {
        public OrderRequestProfile()
        {
            CreateMap<CreateOrderRequestCommand, OrderRequest>()
                .ForMember(o => o.Products, s => s.Ignore());
        }
    }
}
