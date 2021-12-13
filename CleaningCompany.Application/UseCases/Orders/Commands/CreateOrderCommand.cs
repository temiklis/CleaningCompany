using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Result<int>>
    {
        public int OrderRequestId { get; set; }

        public ICollection<string> Employees { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await _unitOfWork.OrderRequests.GetOrderRequestWithProductsAndMaterials(request.OrderRequestId);

            if (request.Employees == null || !request.Employees.Any())
            {
                return new ErrorResult<int>("Employees should be provided");
            }

            if (orderRequest == null)
            {
                return new ErrorResult<int>("Order request doesn't exist");
            }

            var client = await _unitOfWork.Clients.GetClientByEmailAsync(orderRequest.Email);

            if (client == null)
            {
                return new ErrorResult<int>("This client doesn't exist");
            }

            var employees = await _unitOfWork.Employees.FindAsync(e => request.Employees.Contains(e.Id));

            if (request.Employees.Count != employees.Count())
            {
                return new ErrorResult<int>("some employees doesn't exist");
            }

            var order = new Order()
            {
                Products = orderRequest.Products,
                ClientId = client.Id,
                ResponsibleEmployees = employees.ToList(),
                RenderedDate = orderRequest.RenderedDate,
                OrderDate = DateTime.Now,
                OrderRequestId = orderRequest.Id,
                Status = Status.NotStarted,
                TotalPrice = CalculateTotalPriceForOrderRequest(orderRequest)
            };


            orderRequest.RenderedDate = order.OrderDate;

            var createdOrder = await _unitOfWork.Orders.CreateAsync(order);

            try
            {
                await _unitOfWork.Orders.SaveChangesAsync();
                await _unitOfWork.OrderRequests.SaveChangesAsync();

                var message = $"Responsibe Employees were assigned to your order! We will provide you with order status!";

                await _emailService.SendEmailAsync(orderRequest.Email, "Cleaning Order", message);
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(createdOrder.Id);
        }

        private decimal CalculateTotalPriceForOrderRequest(OrderRequest orderRequest)
        {
            decimal totalPrice = 0;
            foreach (var product in orderRequest.Products)
            {
                totalPrice += product.BasePrice;

                if (product.Materials == null)
                    continue;

                foreach (var material in product.Materials)
                {
                    totalPrice += material.Price;
                }
            }

            return totalPrice;
        }
    }
}
