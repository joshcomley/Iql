using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.Validation
{
    public class ValidationResult : IValidationResult
    {
        public List<ValidationError> ValidationFailures { get; set; }

        public void AddFailure(string message)
        {
            if (ValidationFailures == null)
            {
                ValidationFailures = new List<ValidationError>();
            }
            ValidationFailures.Add(new ValidationError(message));
        }

        public virtual bool HasValidationFailures()
        {
            return ValidationFailures.Any();
        }
    }
}