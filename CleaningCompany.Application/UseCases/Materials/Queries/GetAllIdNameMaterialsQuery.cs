using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllIdNameMaterialsQuery : IRequest<Result<IEnumerable<MaterialIdNameDto>>>
    {

    }

    public class GetAllIdNameMaterialsQueryHandler : IRequestHandler<GetAllIdNameMaterialsQuery, Result<IEnumerable<MaterialIdNameDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllIdNameMaterialsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<MaterialIdNameDto>>> Handle(GetAllIdNameMaterialsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Materials.GetAllAsync();

            var dtos = _mapper.Map<IEnumerable<MaterialIdNameDto>>(query);

            return new SuccessResult<IEnumerable<MaterialIdNameDto>>(dtos);
        }
    }
}
