using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public interface IValidationResult
    {
        Type EntityType { get; }
        List<ValidationError> ValidationFailures { get; set; }
        void AddFailure(string key, string message);
        bool HasValidationFailures();
    }
}