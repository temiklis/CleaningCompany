using CleaningCompany.Application.UseCases.Products.Commands;
using CleaningCompany.Domain.Entities.Enums;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.Products.Validation
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Difficulty).NotEmpty().IsEnumName(typeof(Difficulty));
            RuleFor(c => c.BasePrice).NotEmpty();
        }
    }
}
