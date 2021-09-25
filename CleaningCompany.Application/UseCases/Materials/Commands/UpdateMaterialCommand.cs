using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.Validation;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Commands
{
    public class UpdateMaterialCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMaterialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMaterialValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ValidationErrorResult<int>("Validation error occured", validationResult);
            }

            var material = await _unitOfWork.Materials.GetSingleAsync(request.Id);

            if (material == null)
            {
                return new NotFoundResult<int>("Material with given id was not found");
            }

            _mapper.Map(request, material);
            _unitOfWork.Materials.Update(material);

            try
            {
                await _unitOfWork.Materials.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(material.Id);
        }
    }
}
