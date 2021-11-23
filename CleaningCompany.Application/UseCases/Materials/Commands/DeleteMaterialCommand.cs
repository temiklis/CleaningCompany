using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Commands
{
    public class DeleteMaterialCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMaterialCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = new Material { Id = request.Id };

            _unitOfWork.Materials.Delete(material);

            try
            {
                await _unitOfWork.Materials.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(request.Id);
        }
    }
}
