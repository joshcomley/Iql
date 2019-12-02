using System;
using System.Collections.Generic;

namespace Iql.Entities.Validation.Validation
{
    public class RelationshipCollectionValidationResult<T> : PropertyValidationResult<T>, IRelationshipCollectionValidationResult
    {
        public Type RelationshipEntityType { get; }
        private bool _relationshipValidationResultsInitialized;
        private List<RelationshipCollectionValidationResultItem<T>> _relationshipValidationResults;
        public List<RelationshipCollectionValidationResultItem<T>> RelationshipValidationResults { get { if(!_relationshipValidationResultsInitialized) { _relationshipValidationResultsInitialized = true; _relationshipValidationResults = new List<RelationshipCollectionValidationResultItem<T>>(); } return _relationshipValidationResults; } set { _relationshipValidationResultsInitialized = true; _relationshipValidationResults = value; } }
        IEnumerable<IRelationshipCollectionValidationResultItem> IRelationshipCollectionValidationResult.RelationshipValidationResults => RelationshipValidationResults;

        public RelationshipCollectionValidationResult(Type relationshipEntityType, T rootEntity, IProperty property)
            : base(rootEntity, property)
        {
            RelationshipEntityType = relationshipEntityType;
        }
    }
}