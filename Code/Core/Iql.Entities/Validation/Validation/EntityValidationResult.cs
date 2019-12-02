using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Validation.Validation
{
    public class EntityValidationResult<T> : ValidationResult<T, EntityValidationResult<T>>, IEntityValidationResult
    {
        private List<PropertyValidationResult<T>> _propertyValidationResults = null;
        public List<PropertyValidationResult<T>> PropertyValidationResults { get => _propertyValidationResults = _propertyValidationResults ?? new List<PropertyValidationResult<T>>(); set => _propertyValidationResults = value; }
        private List<RelationshipValidationResult<T>> _relationshipValidationResults = null;
        public List<RelationshipValidationResult<T>> RelationshipValidationResults { get => _relationshipValidationResults = _relationshipValidationResults ?? new List<RelationshipValidationResult<T>>(); set => _relationshipValidationResults = value; }
        private List<RelationshipCollectionValidationResult<T>> _relationshipCollectionValidationResults = null;
        public List<RelationshipCollectionValidationResult<T>> RelationshipCollectionValidationResults { get => _relationshipCollectionValidationResults = _relationshipCollectionValidationResults ?? new List<RelationshipCollectionValidationResult<T>>(); set => _relationshipCollectionValidationResults = value; }

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