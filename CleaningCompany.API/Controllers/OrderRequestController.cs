using CleaningCompany.Application.UseCases.OrderRequests;
using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using CleaningCompany.Application.UseCases.OrderRequests.Queries;
using CleaningCompany.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderRequestController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderRequestDto createOrderRequestDto)
        {
            var id = await _mediator.Send(new CreateOrderRequestCommand()
            {
                FIO = createOrderRequestDto.FIO,
                Address = createOrderRequestDto.Address,
                RequestedDate = createOrderRequestDto.RequestedDate,
                Email = createOrderRequestDto.Email,
                Products = createOrderRequestDto.Products.Select(p => p.Id).ToList()
            });

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRequestDto>>> GetAllOrderRequests([FromQuery] OrderRequestParameters parameters)
        {
            var result = await _mediator.Send(new GetAllOrderRequestsQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<OrderRequestDto>>(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRequestDetailsDto>> GetOrderRequestById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetOrderRequestByIdQuery() { Id = id });

            return CreateResponseFromResult<OrderRequestDetailsDto>(result);
        }

        [HttpGet("Client")]
        public async Task<ActionResult<IEnumerable<UserOrderRequestDto>>> GetClientOrderRequests([FromQuery] string email)
        {
            var orderRequests = await _mediator.Send(new GetUserOrderRequestsQuery()
            {
                Email = email
            });

            return Ok(orderRequests);
        }
    }
}
