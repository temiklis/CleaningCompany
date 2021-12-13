using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.OrderRequests.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllOrderRequestsQuery : IRequest<Result<PagedList<OrderRequestDto>>>
    {
        public OrderRequestParameters Parameters { get; private set; }

        public GetAllOrderRequestsQuery(OrderRequestParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllOrderRequestsQueryHandler : IRequestHandler<GetAllOrderRequestsQuery, Result<PagedList<OrderRequestDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrderRequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<OrderRequestDto>>> Handle(GetAllOrderRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.OrderRequests.GetOrderRequestsWithProducts();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<OrderRequest>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<OrderRequestDto>>);

            return new SuccessResult<PagedList<OrderRequestDto>>(dtos);
        }

        private static IQueryable<OrderRequest> GetFilters(IQueryable<OrderRequest> query, OrderRequestParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Address))
            {
                query = query.Where(s => s.Address.ToLower().Contains(parameters.Address.ToLower()));
            }

            if (!string.IsNullOrEmpty(parameters.FIO))
            {
                query = query.Where(s => s.FIO.ToLower().Contains(parameters.FIO.ToLower()));
            }

            if (!string.IsNullOrEmpty(parameters.Email))
            {
                query = query.Where(s => s.Email.ToLower().Contains(parameters.Email.ToLower()));
            }

            return query.OrderByDescending(or => or.RequestedDate).AsQueryable();
        }
    }
}
