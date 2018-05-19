using System;
using System.Collections.Generic;

namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IValidationResult
    {
        Type EntityType { get; }
        List<ValidationError> ValidationFailures { get; set; }
        IValidationResult AddFailure(string key, string message);
        bool HasValidationFailures();
    }
}