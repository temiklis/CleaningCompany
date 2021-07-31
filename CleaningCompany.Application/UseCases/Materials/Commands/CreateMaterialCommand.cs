using CleaningCompany.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Materials.Commands
{
    public class CreateMaterialCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, int>
    {
        private readonly IMaterialRepository _repository;

        public CreateMaterialCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
