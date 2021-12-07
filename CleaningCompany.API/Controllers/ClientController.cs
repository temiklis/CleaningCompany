using CleaningCompany.Application.UseCases.Clients;
using CleaningCompany.Application.UseCases.Clients.DTOs;
using CleaningCompany.Application.UseCases.Clients.Queries;
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
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get([FromQuery] ClientParameters parameters)
        {
            var clients = await _mediator.Send(new GetAllClientsQuery(parameters));

            return Ok(clients);
        }
    }
}