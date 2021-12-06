using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.OrderRequests.Queries
{
    public class GetUserOrderRequestsQuery : IRequest<IEnumerable<UserOrderRequestDto>>
    {
        public string Email { get; set; }
    }

    public class GetUserOrderRequestsQueryHandler: IRequestHandler<GetUserOrderRequestsQuery, IEnumerable<UserOrderRequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserOrderRequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserOrderRequestDto>> Handle(GetUserOrderRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.OrderRequests.GetUserOrderRequests(request.Email);

            return _mapper.Map<IEnumerable<UserOrderRequestDto>>(query);
        }
    }
}
