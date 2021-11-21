using CleaningCompany.Application.UseCases.OrderRequests.Commands;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.OrderRequests.Validation
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequestCommand>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.FIO).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
        }
    }
}
