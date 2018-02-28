using System;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Validation
{
    public class RelationshipValidationResult<T> : PropertyValidationResult<T>
    {
        public Type RelationshipEntityType { get; }
        public EntityValidationResult<T> EntityValidationResult { get; set; }

        public RelationshipValidationResult(
            Type relationshipEntityType, 
            T rootEntity,
            EntityValidationResult<T> entityValidationResult, 
            IProperty property) : base(rootEntity, property)
        {
            RelationshipEntityType = relationshipEntityType;
            EntityValidationResult = entityValidationResult;
        }
    }
}