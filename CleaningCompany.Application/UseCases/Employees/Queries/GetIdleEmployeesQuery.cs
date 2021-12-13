using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Employees.DTOs;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Employees.Queries
{
    public class GetIdleEmployeesQuery : IRequest<Result<IEnumerable<IdleEmployeeDto>>>
    {
        public DateTime Date { get; set; }
    }


    public class GetIdleEmployeesQueryHandler : IRequestHandler<GetIdleEmployeesQuery, Result<IEnumerable<IdleEmployeeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetIdleEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<IdleEmployeeDto>>> Handle(GetIdleEmployeesQuery request, CancellationToken cancellationToken)
        {
            var idleEmployeesFromDb = await _unitOfWork.Employees.GetIdleEmployeesByOrderDate(request.Date);

            var idleEmployees = _mapper.Map<IEnumerable<IdleEmployeeDto>>(idleEmployeesFromDb);

            return new SuccessResult<IEnumerable<IdleEmployeeDto>>(idleEmployees);
        }
    }

}
