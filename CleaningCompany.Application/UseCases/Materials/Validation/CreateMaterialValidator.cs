using CleaningCompany.Application.UseCases.Materials.Commands;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.Materials.Validation
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterialCommand>
    {
        public CreateMaterialValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Price).NotEmpty();
        }
    }
}
