using CleaningCompany.Application.UseCases.Materials.Commands;
using FluentValidation;

namespace CleaningCompany.Application.UseCases.Materials.Validation
{
    public class UpdateMaterialValidator : AbstractValidator<UpdateMaterialCommand>
    {
        public UpdateMaterialValidator()
        {
            RuleFor(s => s.Id).NotEmpty();
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Price).NotEmpty();
        }
    }
}
