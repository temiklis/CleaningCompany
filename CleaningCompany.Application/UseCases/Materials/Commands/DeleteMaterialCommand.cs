using CleaningCompany.Domain.Interfaces;
using CleaningCompany.Result;
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
            throw new NotImplementedException();
        }
    }
}
