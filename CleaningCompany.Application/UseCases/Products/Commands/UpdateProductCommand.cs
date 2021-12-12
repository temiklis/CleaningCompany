using AutoMapper;
using CleaningCompany.Application.UseCases.Products.Validation;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Common.Security;

namespace CleaningCompany.Application.UseCases.Products.Commands
{
    [Authorize(Roles = "Admin")]
    public class UpdateProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Difficulty { get; set; }
        public List<int> MaterialsIds { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ValidationErrorResult<int>("Validation error occured", validationResult);
            }

            var product = await _unitOfWork.Products.GetProductWithMaterials(request.Id);

            if (product == null)
            {
                return new NotFoundResult<int>("Product with given id was not found");
            }

            _mapper.Map(request, product);

            var result = await AssignMaterialsToProduct(request.MaterialsIds, product);

            if (!result.Success)
                return result;

            _unitOfWork.Products.Update(product);

            try
            {
                await _unitOfWork.Products.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(product.Id);
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
                product.Materials.Clear();

                return new SuccessResult<int>(default);
            }

            var isAllMaterialExist = materials.Count() == materialsIds.Count;

            if (!isAllMaterialExist)
            {
                var notExistMaterials = materialsIds.Except(materials.Select(m => m.Id));
                return new ErrorResult<int>($"Some materials doesn't exist. Ids: {string.Join(',', notExistMaterials)}");
            }

            product.Materials.Clear();
            foreach (var material in materials)
            {
                product.Materials.Add(material);
            }


            return new SuccessResult<int>(default);
        }
    }
}
