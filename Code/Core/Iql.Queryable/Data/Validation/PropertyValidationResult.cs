using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public class PropertyValidationResult : ValidationResult
    {
        public Type EntityType { get; }
        public string PropertyName { get; set; }

        public PropertyValidationResult(
            Type entityType,
            string propertyName)
        {
            EntityType = entityType;
            PropertyName = propertyName;
        }
    }
}