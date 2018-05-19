using System;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Validation
{
    public class PropertyValidationResult<T> : ValidationResult<T, PropertyValidationResult<T>>, IPropertyValidationResult
    {
        public IProperty Property { get; set; }

        public PropertyValidationResult(
            T entity,
            IProperty property) : base(entity)
        {
            Property = property;
        }
    }
}