using CleaningCompany.Application.UseCases.Employees;
using CleaningCompany.Application.UseCases.Employees.DTOs;
using CleaningCompany.Application.UseCases.Employees.Queries;
using CleaningCompany.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get([FromQuery]EmployeeParameters parameters)
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<EmployeeDto>>(result);
        }
    }
}
