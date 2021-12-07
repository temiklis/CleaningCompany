using CleaningCompany.Application.UseCases.Orders;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Application.UseCases.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get(OrderParameters parameters)
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery(parameters));

            return Ok(orders);
        }

        [HttpGet("Client/{clientId}")]
        public async Task<ActionResult<IEnumerable<ClientOrderDto>>> GetClientOrders([FromRoute] string clientId)
        {
            var orders = await _mediator.Send(new GetClientOrdersQuery() { ClientId = clientId });

            return Ok(orders);
        }

        [HttpGet("Employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeAssignedOrderDto>>> GetEmployeeAssignedOrders([FromRoute] string employeeId)
        {
            var orders = await _mediator.Send(new GetEmployeeAssignedOrdersQuery() { EmployeeId = employeeId });

            return Ok(orders);
        }
    }
}
