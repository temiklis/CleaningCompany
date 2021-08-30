using CleaningCompany.Application.UseCases.Materials.DTOs;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.Materials.Validation
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterialDto>
    {
        public CreateMaterialValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Price).NotEmpty();
        }
    }
}
