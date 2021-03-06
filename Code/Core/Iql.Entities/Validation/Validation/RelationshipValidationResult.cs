﻿using System;

namespace Iql.Entities.Validation.Validation
{
    public class RelationshipValidationResult<T> : PropertyValidationResult<T>, IRelationshipValidationResult
    {
        public Type RelationshipEntityType { get; }
        public EntityValidationResult<T> EntityValidationResult { get; set; }
        IEntityValidationResult IRelationshipValidationResult.EntityValidationResult => EntityValidationResult;

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