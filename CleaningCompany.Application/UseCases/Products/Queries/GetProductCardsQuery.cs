using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Products.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Products.Queries
{
    public class GetProductCardsQuery : IRequest<IEnumerable<ProductCardDto>>
    {

    }

    public class GetProductCardsQueryHandler : IRequestHandler<GetProductCardsQuery, IEnumerable<ProductCardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductCardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCardDto>> Handle(GetProductCardsQuery request, CancellationToken cancellationToken)
        {
            var dbProducts = await _unitOfWork.Products.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductCardDto>>(dbProducts);
        }
    }
}
