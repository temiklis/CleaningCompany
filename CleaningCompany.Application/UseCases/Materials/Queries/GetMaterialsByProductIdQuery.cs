using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Queries
{
    public class GetMaterialsByProductIdQuery : IRequest<IEnumerable<MaterialDto>>
    {
        public int Id { get; set; }
    }

    public class GetMaterialsByProductIdRequestHandler : IRequestHandler<GetMaterialsByProductIdQuery, IEnumerable<MaterialDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMaterialsByProductIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialDto>> Handle(GetMaterialsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var dbMaterials = await _unitOfWork.Materials.GetMaterialsForProduct(request.Id);

            return _mapper.Map<List<MaterialDto>>(dbMaterials);
        }
    }
}
