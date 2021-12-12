using CleaningCompany.Application.UseCases.Products.Commands;
using CleaningCompany.Domain.Entities.Enums;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.Products.Validation
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Difficulty).NotEmpty().IsEnumName(typeof(Difficulty));
            RuleFor(c => c.BasePrice).NotEmpty();
        }
    }
}