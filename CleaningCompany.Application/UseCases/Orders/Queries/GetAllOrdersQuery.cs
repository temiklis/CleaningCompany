using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllOrdersQuery : IRequest<Result<PagedList<OrderDto>>>
    {
        public OrderParameters Parameters { get; private set; }

        public GetAllOrdersQuery(OrderParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<PagedList<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Orders.GetAllOrders();

            var dtos = await PagedList<Order>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<OrderDto>>);

            return new SuccessResult<PagedList<OrderDto>>(dtos);
        }

        private static IQueryable<Order> GetFilters(IQueryable<Order> query, OrderParameters parameters)
        {
            return query;
        }
    }
}
