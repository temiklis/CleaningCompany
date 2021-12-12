using AutoMapper;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleaningCompany.Application.Common.Security;

namespace CleaningCompany.Application.UseCases.Products.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetProductByIdQuery : IRequest<ProductWithMaterialsDto>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductWithMaterialsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductWithMaterialsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var dbProduct = await _unitOfWork.Products.GetProductWithMaterials(request.Id);

            return _mapper.Map<ProductWithMaterialsDto>(dbProduct);
        }
    }
}
