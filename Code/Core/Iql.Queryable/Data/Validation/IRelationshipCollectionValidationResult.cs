using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public interface IRelationshipCollectionValidationResult
    {
        Type RelationshipEntityType { get; }
        IEnumerable<IRelationshipCollectionValidationResultItem> RelationshipValidationResults { get; }
    }
}