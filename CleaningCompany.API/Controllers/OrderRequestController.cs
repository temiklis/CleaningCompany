using CleaningCompany.Application.UseCases.OrderRequests;
using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
using CleaningCompany.Application.UseCases.OrderRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<IEnumerable<OrderRequestDto>>> Get([FromQuery] OrderRequestParameters parameters)
        {
            var products = await _mediator.Send(new GetAllOrderRequestsQuery(parameters));

            return Ok(products);
        }
    }
}
