using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.Validation
{
    public class EntityValidationResult<T> : ValidationResult<T>, IEntityValidationResult
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

        void IEntityValidationResult.AddPropertyValidationResult(IPropertyValidationResult result)
        {
            AddPropertyValidationResult((PropertyValidationResult<T>) result);
        }

        public void AddPropertyValidationResult(PropertyValidationResult<T> result)
        {
            if (PropertyValidationResults == null)
            {
                PropertyValidationResults = new List<PropertyValidationResult<T>>();
            }
            PropertyValidationResults.Add(result);
        }

        public override bool HasValidationFailures()
        {
            return base.HasValidationFailures() || PropertyValidationResults.Any(p => p.HasValidationFailures());
        }
    }
}