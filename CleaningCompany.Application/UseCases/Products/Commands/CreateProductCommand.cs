using AutoMapper;
using CleaningCompany.Application.UseCases.Products.Validation;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CleaningCompany.Application.Common.Security;

namespace CleaningCompany.Application.UseCases.Products.Commands
{
    [Authorize(Roles = "Admin")]
    public class CreateProductCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Difficulty { get; set; }
        public List<int> MaterialsIds { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ValidationErrorResult<int>("Validation error occured", validationResult);
            }

            var product = _mapper.Map<Product>(request);
            var productWithTheSameNameExists = await _unitOfWork.Products.AnyAsync(s => s.Name.ToUpper() == request.Name.ToUpper());

            if (productWithTheSameNameExists)
            {
                return new ErrorResult<int>("Product with the same name already exists");
            }

            var result = await AssignMaterialsToProduct(request.MaterialsIds, product);
            if (!result.Success)
                return result;


            var createdProduct = await _unitOfWork.Products.CreateAsync(product);

            try
            {
                await _unitOfWork.Products.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(createdProduct.Id);
        }


        private async Task<Result<int>> AssignMaterialsToProduct(List<int> materialsIds, Product product)
        {
            IEnumerable<Material> materials = Enumerable.Empty<Material>();

            if (materialsIds != null && materialsIds.Count > 0)
            {
                materials = await _unitOfWork.Materials.FindAsync(m => materialsIds.Contains(m.Id));          
            }
            else
            {
                return new SuccessResult<int>(default);
            }

            var isAllMaterialExist = materials.Count() == materialsIds.Count;

            if (!isAllMaterialExist)
            {
                var notExistMaterials = materialsIds.Except(materials.Select(m => m.Id));
                return new ErrorResult<int>($"Some meterials doesn't exist. Ids: {string.Join(',', notExistMaterials)}");
            }

            product.Materials = new List<Material>();
            foreach (var material in materials)
            {
                product.Materials.Add(material);
            }


            return new SuccessResult<int>(default);
        }
    }
}
