using AutoMapper;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CleaningCompany.Results;

namespace CleaningCompany.Application.UseCases.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public ProductParameters Parameters { get; private set; }
        public GetAllProductsQuery(ProductParameters parameters)
        {
            Parameters = parameters;
        }
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
            var query = _unitOfWork.Products.GetAllAsync();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<Product>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<ProductDto>>);

            return dtos;
        }

        private static IQueryable<Product> GetFilters(IQueryable<Product> query, ProductParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(s => s.Name.ToLower().Contains(parameters.Name.ToLower()));
            }

            return query;
        }
    }
}
