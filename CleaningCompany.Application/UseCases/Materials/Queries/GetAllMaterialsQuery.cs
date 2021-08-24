using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Queries
{
    public class GetAllMaterialsQuery : IRequest<IEnumerable<MaterialDto>>
    {

    }

    public class GetAllMaterialsQueryHandler : IRequestHandler<GetAllMaterialsQuery, IEnumerable<MaterialDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMaterialsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialDto>> Handle(GetAllMaterialsQuery request, CancellationToken cancellationToken)
        {
            var dbMaterials = await _unitOfWork.Materials.GetAllAsync();

            return _mapper.Map<IEnumerable<MaterialDto>>(dbMaterials);
        }
    }
}
