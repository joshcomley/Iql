using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public class PropertyValidationResult : ValidationResult
    {
        public string EntityType { get; }
        public string PropertyName { get; set; }

        public PropertyValidationResult(
            string entityType,
            string propertyName)
        {
            EntityType = entityType;
            PropertyName = propertyName;
        }
    }
}