using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities.Enums;
using CleaningCompany.Result;
using CleaningCompany.Result.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Commands
{
    [Authorize(Roles = "Employee")]
    public class UpdateOrderStatusCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var dbOrder = await _unitOfWork.Orders.GetOrderWithClient(request.Id);

            dbOrder.Status = (Status)Enum.Parse(typeof(Status), request.Status);

            _unitOfWork.Orders.Update(dbOrder);

            try
            {
                await _unitOfWork.Orders.SaveChangesAsync();

                var message = $"Your order status was changed to '{request.Status}'!";

                await _emailService.SendEmailAsync(dbOrder.Client.Email, "Order Status Update", message);

            }
            catch (Exception ex)
            {
                return new ErrorResult<int>(ex.Message);
            }

            return new SuccessResult<int>(dbOrder.Id);
        }
    }
}
