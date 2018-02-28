using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.Validation
{
    public abstract class ValidationResult<T> : IValidationResult
    {
        public T Entity { get; set; }
        public Type EntityType => typeof(T);
        public List<ValidationError> ValidationFailures { get; set; } = new List<ValidationError>();

        protected ValidationResult(T entity)
        {
            Entity = entity;
        }

        public void AddFailure(string key, string message)
        {
            if (ValidationFailures == null)
            {
                ValidationFailures = new List<ValidationError>();
            }
            ValidationFailures.Add(new ValidationError(key, message));
        }

        public virtual bool HasValidationFailures()
        {
            return ValidationFailures.Any();
        }
    }
}