using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Employees.DTOs;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Employees.Queries
{
    public class GetIdleEmployeesQuery : IRequest<Result<IEnumerable<EmployeeDto>>>
    {
        public DateTime Date { get; set; }
    }


    public class GetIdleEmployeesQueryHandler : IRequestHandler<GetIdleEmployeesQuery, Result<IEnumerable<EmployeeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetIdleEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> Handle(GetIdleEmployeesQuery request, CancellationToken cancellationToken)
        {
            var idleEmployeesFromDb = await _unitOfWork.Employees.GetIdleEmployeesByOrderDate(request.Date);

            var idleEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(idleEmployeesFromDb);

            return new SuccessResult<IEnumerable<EmployeeDto>>(idleEmployees);
        }
    }

}
