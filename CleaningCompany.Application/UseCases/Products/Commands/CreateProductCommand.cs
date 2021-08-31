using AutoMapper;
using CleaningCompany.Application.UseCases.Products.Validation;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Products.Commands
{
    public class CreateProductCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Difficulty { get; set; }
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
    }
}
