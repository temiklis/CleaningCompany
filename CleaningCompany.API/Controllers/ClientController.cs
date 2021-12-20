using CleaningCompany.Application.UseCases.Clients;
using CleaningCompany.Application.UseCases.Clients.DTOs;
using CleaningCompany.Application.UseCases.Clients.Queries;
using CleaningCompany.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : BaseController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients([FromQuery] ClientParameters parameters)
        {
            var result = await _mediator.Send(new GetAllClientsQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<ClientDto>>(result);
        }
    }
}