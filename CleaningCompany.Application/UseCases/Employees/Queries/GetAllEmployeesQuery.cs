using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Employees.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Employees.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllEmployeesQuery : IRequest<Result<PagedList<EmployeeDto>>>
    {
        public EmployeeParameters Parameters { get; private set; }
        public GetAllEmployeesQuery(EmployeeParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Result<PagedList<EmployeeDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEmployeesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Employees.GetAllAsync();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<Employee>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<EmployeeDto>>);

            return new SuccessResult<PagedList<EmployeeDto>>(dtos);
        }

        private static IQueryable<Employee> GetFilters(IQueryable<Employee> query, EmployeeParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(s => s.FirstName.ToLower().Contains(parameters.Name.ToLower())
                || s.LastName.ToLower().Contains(parameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(parameters.Email))
            {
                query = query.Where(s => s.Email.ToLower().Contains(parameters.Email.ToLower()));
            }

            return query;
        }
    }

}
