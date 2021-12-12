using CleaningCompany.Application.UseCases.Orders;
using CleaningCompany.Application.UseCases.Orders.Commands;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Application.UseCases.Orders.Queries;
using CleaningCompany.Results;
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

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateOrderDto createOrderDto)
        {
            var command = new CreateOrderCommand()
            {
                OrderRequestId = createOrderDto.OrderRequestId,
                Employees = createOrderDto.Employees
            };

            var result = await _mediator.Send(command);

            return CreateResponseFromResult<int>(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get(OrderParameters parameters)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<OrderDto>>(result);
        }

        [HttpGet("Client")]
        public async Task<ActionResult<IEnumerable<ClientOrderDto>>> GetClientOrders()
        {
            var orders = await _mediator.Send(new GetClientOrdersQuery());

            return Ok(orders);
        }

        [HttpGet("Employee")]
        public async Task<ActionResult<IEnumerable<EmployeeAssignedOrderDto>>> GetEmployeeAssignedOrders()
        {
            var orders = await _mediator.Send(new GetEmployeeAssignedOrdersQuery());

            return Ok(orders);
        }
    }
}
