using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using CleaningCompany.Application.UseCases.OrderRequests.DTOs;
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
                Date = DateTime.Now,
                Email = createOrderRequestDto.Email,
                Products = createOrderRequestDto.Products.Select(p => p.Id).ToList()
            });

            return Ok(id);
        }
    }
}
