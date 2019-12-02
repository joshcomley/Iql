using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Validation.Validation
{
    public class EntityValidationResult<T> : ValidationResult<T, EntityValidationResult<T>>, IEntityValidationResult
    {
        private bool _propertyValidationResultsInitialized;
        private List<PropertyValidationResult<T>> _propertyValidationResults;
        public List<PropertyValidationResult<T>> PropertyValidationResults { get { if(!_propertyValidationResultsInitialized) { _propertyValidationResultsInitialized = true; _propertyValidationResults = new List<PropertyValidationResult<T>>(); } return _propertyValidationResults; } set { _propertyValidationResultsInitialized = true; _propertyValidationResults = value; } }
        private bool _relationshipValidationResultsInitialized;
        private List<RelationshipValidationResult<T>> _relationshipValidationResults;
        public List<RelationshipValidationResult<T>> RelationshipValidationResults { get { if(!_relationshipValidationResultsInitialized) { _relationshipValidationResultsInitialized = true; _relationshipValidationResults = new List<RelationshipValidationResult<T>>(); } return _relationshipValidationResults; } set { _relationshipValidationResultsInitialized = true; _relationshipValidationResults = value; } }
        private bool _relationshipCollectionValidationResultsInitialized;
        private List<RelationshipCollectionValidationResult<T>> _relationshipCollectionValidationResults;
        public List<RelationshipCollectionValidationResult<T>> RelationshipCollectionValidationResults { get { if(!_relationshipCollectionValidationResultsInitialized) { _relationshipCollectionValidationResultsInitialized = true; _relationshipCollectionValidationResults = new List<RelationshipCollectionValidationResult<T>>(); } return _relationshipCollectionValidationResults; } set { _relationshipCollectionValidationResultsInitialized = true; _relationshipCollectionValidationResults = value; } }

        public EntityValidationResult(T entity) : base(entity)
        {
        }

        IEnumerable<IPropertyValidationResult> IEntityValidationResult.PropertyValidationResults => PropertyValidationResults;
        IEnumerable<IRelationshipValidationResult> IEntityValidationResult.RelationshipValidationResults => RelationshipValidationResults;
        IEnumerable<IRelationshipCollectionValidationResult> IEntityValidationResult.RelationshipCollectionValidationResults => RelationshipCollectionValidationResults;

        void IEntityValidationResult.AddPropertyValidationResult(IPropertyValidationResult result)
        {
            AddPropertyValidationResult((PropertyValidationResult<T>) result);
        }

        public EntityValidationResult<T> AddPropertyValidationResult(PropertyValidationResult<T> result)
        {
            if (PropertyValidationResults == null)
            {
                PropertyValidationResults = new List<PropertyValidationResult<T>>();
            }
            PropertyValidationResults.Add(result);
            return this;
        }

        public override bool HasValidationFailures()
        {
            return base.HasValidationFailures() || PropertyValidationResults.Any(p => p.HasValidationFailures());
        }
    }
}