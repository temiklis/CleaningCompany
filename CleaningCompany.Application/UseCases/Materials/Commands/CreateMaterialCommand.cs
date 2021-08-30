using AutoMapper;
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
            throw new NotImplementedException();
        }
    }
}
