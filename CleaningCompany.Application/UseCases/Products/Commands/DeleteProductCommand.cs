using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product() { Id = request.Id };

            _unitOfWork.Products.Delete(product);

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
    }
}
