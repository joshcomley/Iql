using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.Validation
{
    public class EntityValidationResult : ValidationResult
    {
        public List<PropertyValidationResult> PropertyValidationResults { get; set; }
            = new List<PropertyValidationResult>();

        public Type EntityType { get; set; }

        public EntityValidationResult(Type entityType)
        {
            EntityType = entityType;
        }

        public void AddPropertyValidationResult(PropertyValidationResult result)
        {
            if (PropertyValidationResults == null)
            {
                PropertyValidationResults = new List<PropertyValidationResult>();
            }
            PropertyValidationResults.Add(result);
        }

        public override bool HasValidationFailures()
        {
            return base.HasValidationFailures() || PropertyValidationResults.Any(p => p.HasValidationFailures());
        }
    }
}