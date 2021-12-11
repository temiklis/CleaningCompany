using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Queries
{
    [Authorize(Roles ="User,Admin")]
    public class GetClientOrdersQuery : IRequest<IEnumerable<ClientOrderDto>>
    {

    }

    public class GetClientOrdersQueryHandler : IRequestHandler<GetClientOrdersQuery, IEnumerable<ClientOrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetClientOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<ClientOrderDto>> Handle(GetClientOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var query = _unitOfWork.Orders.GetClientOrders(userId);

            return _mapper.Map<IEnumerable<ClientOrderDto>>(query);
        }
    }

}
