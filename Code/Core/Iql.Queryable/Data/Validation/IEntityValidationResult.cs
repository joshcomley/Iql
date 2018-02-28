using System.Collections.Generic;

namespace Iql.Queryable.Data.Validation
{
    public interface IEntityValidationResult
    {
        IEnumerable<IPropertyValidationResult> PropertyValidationResults { get; }
        void AddPropertyValidationResult(IPropertyValidationResult result);
        bool HasValidationFailures();
    }
}