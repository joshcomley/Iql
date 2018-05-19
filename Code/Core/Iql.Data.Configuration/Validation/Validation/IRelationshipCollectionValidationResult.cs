using System;
using System.Collections.Generic;

namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IRelationshipCollectionValidationResult
    {
        Type RelationshipEntityType { get; }
        IEnumerable<IRelationshipCollectionValidationResultItem> RelationshipValidationResults { get; }
    }
}