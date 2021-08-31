using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Queries
{
    public class GetMaterialByIdQuery : IRequest<MaterialWithProductsDto>
    {
        public int Id { get; set; }
    }

    public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, MaterialWithProductsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMaterialByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MaterialWithProductsDto> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var dbMaterial = await _unitOfWork.Materials.GetSingleAsync(request.Id);

            return _mapper.Map<MaterialWithProductsDto>(dbMaterial);
        }
    }
}
