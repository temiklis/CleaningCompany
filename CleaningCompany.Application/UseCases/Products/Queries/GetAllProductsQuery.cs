using AutoMapper;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {

    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var dbProducts = await _unitOfWork.Products.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(dbProducts);
        }
    }
}
