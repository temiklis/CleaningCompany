using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public OrderParameters Parameters { get; private set; }

        public GetAllOrdersQuery(OrderParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Orders.GetAllOrders();

            var dtos = await PagedList<Order>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<OrderDto>>);

            return dtos;
        }

        private static IQueryable<Order> GetFilters(IQueryable<Order> query, OrderParameters parameters)
        {
            return query;
        }
    }
}
