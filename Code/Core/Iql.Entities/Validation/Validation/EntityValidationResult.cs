using System.Collections.Generic;
using System.Linq;

namespace Iql.Data.Configuration.Validation.Validation
{
    public class EntityValidationResult<T> : ValidationResult<T, EntityValidationResult<T>>, IEntityValidationResult
    {
        public List<PropertyValidationResult<T>> PropertyValidationResults { get; private set; }
            = new List<PropertyValidationResult<T>>();
        public List<RelationshipValidationResult<T>> RelationshipValidationResults { get; set; }
            = new List<RelationshipValidationResult<T>>();
        public List<RelationshipCollectionValidationResult<T>> RelationshipCollectionValidationResults { get; set; }
            = new List<RelationshipCollectionValidationResult<T>>();

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