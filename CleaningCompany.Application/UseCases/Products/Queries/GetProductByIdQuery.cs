using AutoMapper;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var dbProduct = await _unitOfWork.Products.GetProductWithMaterials(request.Id);

            return _mapper.Map<ProductDto>(dbProduct);
        }
    }
}
