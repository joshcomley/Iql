using System;

namespace Iql.Queryable.Data.Validation
{
    public class RelationshipValidationResult : PropertyValidationResult
    {
        public Type RelationshipEntityType { get; }
        public EntityValidationResult EntityValidationResult { get; set; }

        public RelationshipValidationResult(Type relationshipEntityType, Type entityType, EntityValidationResult entityValidationResult, string propertyName) : base(entityType, propertyName)
        {
            RelationshipEntityType = relationshipEntityType;
            EntityValidationResult = entityValidationResult;
        }
    }
}