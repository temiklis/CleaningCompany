using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.OrderRequests.Validation;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.OrderRequests.Commands
{
    public class CreateOrderRequestCommand : IRequest<Result<int>>
    {
        public string Email { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public ICollection<int> Products { get; set; }
    }

    public class CreateOrderRequestCommandHandler : IRequestHandler<CreateOrderRequestCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateOrderRequestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(CreateOrderRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ValidationErrorResult<int>("Validation error occured", validationResult);
            }

            var orderRequest = _mapper.Map<OrderRequest>(request);
            var products = await _unitOfWork.Products.GetProductsByIds(request.Products.ToList());

            orderRequest.Products = products;
            orderRequest.RequestedDate = DateTime.Now;

            var createdOrderRequest = await _unitOfWork.OrderRequests.CreateAsync(orderRequest);

            try
            {
                await _unitOfWork.OrderRequests.SaveChangesAsync();

                var message = $"Your order request was successfully created! Please, wait, we will contact you!";

                await _emailService.SendEmailAsync(request.Email, "Order Request successful creation", message);
            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(createdOrderRequest.Id);
        }
    }
}
