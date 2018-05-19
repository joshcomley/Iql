using System;
using System.Collections.Generic;

namespace Iql.Entities.Validation.Validation
{
    public interface IRelationshipCollectionValidationResult
    {
        Type RelationshipEntityType { get; }
        IEnumerable<IRelationshipCollectionValidationResultItem> RelationshipValidationResults { get; }
    }
}