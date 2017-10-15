using System;

namespace Iql.Queryable.Data.Validation
{
    public interface IPropertyValidationResult : IValidationResult
    {
        Type EntityType { get; }
        string PropertyName { get; set; }
    }
}