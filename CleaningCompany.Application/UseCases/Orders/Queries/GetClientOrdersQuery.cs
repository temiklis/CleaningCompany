using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Queries
{
    public class GetClientOrdersQuery : IRequest<IEnumerable<ClientOrderDto>>
    {
        public string ClientId { get; set; }
    }

    public class GetClientOrdersQueryHandler : IRequestHandler<GetClientOrdersQuery, IEnumerable<ClientOrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetClientOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClientOrderDto>> Handle(GetClientOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Orders.GetClientOrders(request.ClientId);

            return _mapper.Map<IEnumerable<ClientOrderDto>>(query);
        }
    }

}
