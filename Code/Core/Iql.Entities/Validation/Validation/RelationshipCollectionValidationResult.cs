using System;
using System.Collections.Generic;

namespace Iql.Entities.Validation.Validation
{
    public class RelationshipCollectionValidationResult<T> : PropertyValidationResult<T>, IRelationshipCollectionValidationResult
    {
        public Type RelationshipEntityType { get; }
        private List<RelationshipCollectionValidationResultItem<T>> _relationshipValidationResults = null;
        public List<RelationshipCollectionValidationResultItem<T>> RelationshipValidationResults { get => _relationshipValidationResults = _relationshipValidationResults ?? new List<RelationshipCollectionValidationResultItem<T>>(); set => _relationshipValidationResults = value; }
        IEnumerable<IRelationshipCollectionValidationResultItem> IRelationshipCollectionValidationResult.RelationshipValidationResults => RelationshipValidationResults;

        public RelationshipCollectionValidationResult(Type relationshipEntityType, T rootEntity, IProperty property)
            : base(rootEntity, property)
        {
            RelationshipEntityType = relationshipEntityType;
        }
    }
}