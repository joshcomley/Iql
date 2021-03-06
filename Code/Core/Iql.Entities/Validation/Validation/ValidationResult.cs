using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Validation.Validation
{
    public abstract class ValidationResult<T, TThis> : IValidationResult
        where TThis : ValidationResult<T, TThis>
    {
        public T Entity { get; set; }
        public Type EntityType => typeof(T);
        private bool _validationFailuresInitialized;
        private List<ValidationError> _validationFailures;
        public List<ValidationError> ValidationFailures { get { if(!_validationFailuresInitialized) { _validationFailuresInitialized = true; _validationFailures = new List<ValidationError>(); } return _validationFailures; } set { _validationFailuresInitialized = true; _validationFailures = value; } }

        protected ValidationResult(T entity)
        {
            Entity = entity;
        }

        public TThis AddFailure(string key, string message)
        {
            if (ValidationFailures == null)
            {
                ValidationFailures = new List<ValidationError>();
            }
            ValidationFailures.Add(new ValidationError(key, message));
            return (TThis)this;
        }

        IValidationResult IValidationResult.AddFailure(string key, string message)
        {
            return AddFailure(key, message);
        }

        public virtual bool HasValidationFailures()
        {
            return ValidationFailures.Any();
        }
    }
}