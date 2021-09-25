using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.Validation;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Commands
{
    public class CreateMaterialCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaterialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMaterialValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ValidationErrorResult<int>("Validation error occured", validationResult);
            }

            var material = _mapper.Map<Material>(request);
            var materialWithTheSameNameExists = await _unitOfWork.Materials.AnyAsync(m => m.Name.ToUpper() == request.Name.ToUpper());

            if (materialWithTheSameNameExists)
            {
                return new ErrorResult<int>("Material with the same name already exists");
            }

            var createdMaterial = await _unitOfWork.Materials.CreateAsync(material);

            try
            {
                await _unitOfWork.Materials.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(createdMaterial.Id);
        }
    }
}
