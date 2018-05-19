using System.Collections.Generic;

namespace Iql.Data.Configuration.Validation.Validation
{
    public interface IEntityValidationResult
    {
        IEnumerable<IPropertyValidationResult> PropertyValidationResults { get; }
        IEnumerable<IRelationshipValidationResult> RelationshipValidationResults { get; }
        IEnumerable<IRelationshipCollectionValidationResult> RelationshipCollectionValidationResults { get; }
        void AddPropertyValidationResult(IPropertyValidationResult result);
        bool HasValidationFailures();
    }
}