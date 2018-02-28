using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Validation
{
    public class RelationshipCollectionValidationResult<T> : PropertyValidationResult<T>, IRelationshipValidationResult
    {
        public Type RelationshipEntityType { get; }
        public Dictionary<int, RelationshipValidationResult<T>> RelationshipValidationResults { get; set; } = new Dictionary<int, RelationshipValidationResult<T>>();
        public RelationshipCollectionValidationResult(Type relationshipEntityType, T rootEntity, IProperty property) 
            : base(rootEntity, property)
        {
            RelationshipEntityType = relationshipEntityType;
        }
    }
}