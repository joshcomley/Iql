using System;

namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IRelationshipValidationResult: IPropertyValidationResult
    {
        Type RelationshipEntityType { get; }
        IEntityValidationResult EntityValidationResult { get; }
    }
}