using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public class RelationshipCollectionValidationResult : PropertyValidationResult, IRelationshipValidationResult
    {
        public Type RelationshipEntityType { get; }
        public Dictionary<int, RelationshipValidationResult> RelationshipValidationResults { get; set; } = new Dictionary<int, RelationshipValidationResult>();
        public RelationshipCollectionValidationResult(Type relationshipEntityType, Type entityType, string propertyName) 
            : base(entityType, propertyName)
        {
            RelationshipEntityType = relationshipEntityType;
        }
    }
}