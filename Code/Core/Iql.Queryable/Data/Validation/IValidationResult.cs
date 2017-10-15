using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public interface IValidationResult
    {
        List<ValidationError> ValidationFailures { get; set; }

        void AddFailure(string message);
        bool HasValidationFailures();
    }
}