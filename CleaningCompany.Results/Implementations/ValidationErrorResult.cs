using FluentValidation.Results;
using System.Collections.ObjectModel;
using System.Linq;

namespace CleaningCompany.Result.Implementations
{
    public class ValidationErrorResult : ErrorResult
    {
        public ValidationErrorResult(string message) : base(message)
        {

        }

        public ValidationErrorResult(string message, ValidationResult validationResult) : base(message, null)
        {
            var errorsFromResult = validationResult.Errors.Select(vr => new ValidationError(vr.PropertyName, vr.ErrorMessage));
            foreach (var validationError in errorsFromResult)
            {
                Errors.Add(validationError);
            }
        }
    }

    public class ValidationErrorResult<T> : ErrorResult<T>
    {
        public ValidationErrorResult(string message) : base(message)
        {

        }

        public ValidationErrorResult(string message, ValidationResult validationResult) : base(message, null)
        {
            var validationErrors = validationResult.Errors.Select(vr => new ValidationError(vr.PropertyName, vr.ErrorMessage));
            var errors = new Collection<Error>();
            foreach (var validationError in validationErrors)
            {
                errors.Add(validationError);
            }

            Errors = errors;
        }
    }
}
