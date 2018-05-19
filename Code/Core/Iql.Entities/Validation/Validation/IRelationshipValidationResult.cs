using System;

namespace Iql.Entities.Validation.Validation
{
    public interface IRelationshipValidationResult: IPropertyValidationResult
    {
        Type RelationshipEntityType { get; }
        IEntityValidationResult EntityValidationResult { get; }
    }
}