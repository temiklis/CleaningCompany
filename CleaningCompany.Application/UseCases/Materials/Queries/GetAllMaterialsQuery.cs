﻿using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleaningCompany.Domain.Entities;
using System.Linq;
using CleaningCompany.Result;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Result.Implementations;

namespace CleaningCompany.Application.UseCases.Materials.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllMaterialsQuery : IRequest<Result<PagedList<MaterialWithProductsStringDto>>>
    {
        public MaterialParameters Parameters { get; private set; }
        public GetAllMaterialsQuery(MaterialParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllMaterialsQueryHandler : IRequestHandler<GetAllMaterialsQuery, Result<PagedList<MaterialWithProductsStringDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMaterialsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<MaterialWithProductsStringDto>>> Handle(GetAllMaterialsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Materials.GetMaterialsWithProducts();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<Material>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<MaterialWithProductsStringDto>>);

            return new SuccessResult<PagedList<MaterialWithProductsStringDto>>(dtos);
        }

        private static IQueryable<Material> GetFilters(IQueryable<Material> query, MaterialParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(s => s.Name.ToLower().Contains(parameters.Name.ToLower()));
            }

            return query;
        }
    }
}
