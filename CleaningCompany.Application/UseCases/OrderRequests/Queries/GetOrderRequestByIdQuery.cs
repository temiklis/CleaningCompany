using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.OrderRequests.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetOrderRequestByIdQuery : IRequest<Result<OrderRequestDetailsDto>>
    {
        public int Id { get; set; }
    }

    public class GetOrderRequestByIdQueryHandler : IRequestHandler<GetOrderRequestByIdQuery, Result<OrderRequestDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderRequestByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<OrderRequestDetailsDto>> Handle(GetOrderRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var dbOrderRequest = await _unitOfWork.OrderRequests.GetOrderRequestWithProducts(request.Id);

            var dto = _mapper.Map<OrderRequestDetailsDto>(dbOrderRequest);

            return new SuccessResult<OrderRequestDetailsDto>(dto);
        }
    }
}
