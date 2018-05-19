using System;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Validation
{
    public interface IPropertyValidationResult : IValidationResult
    {
        IProperty Property { get; set; }
    }
}