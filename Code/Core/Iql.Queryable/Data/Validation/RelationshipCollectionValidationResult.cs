﻿using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Validation
{
    public class RelationshipCollectionValidationResult<T> : PropertyValidationResult<T>, IRelationshipCollectionValidationResult
    {
        public Type RelationshipEntityType { get; }
        public List<RelationshipCollectionValidationResultItem<T>> RelationshipValidationResults { get; set; } = new List<RelationshipCollectionValidationResultItem<T>>();
        IEnumerable<IRelationshipCollectionValidationResultItem> IRelationshipCollectionValidationResult.RelationshipValidationResults => RelationshipValidationResults;

        public RelationshipCollectionValidationResult(Type relationshipEntityType, T rootEntity, IProperty property)
            : base(rootEntity, property)
        {
            RelationshipEntityType = relationshipEntityType;
        }
    }
}