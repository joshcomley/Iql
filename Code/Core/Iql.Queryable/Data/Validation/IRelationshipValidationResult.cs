using System;

namespace Iql.Queryable.Data.Validation
{
    public interface IRelationshipValidationResult: IPropertyValidationResult
    {
        Type RelationshipEntityType { get; }
    }
}