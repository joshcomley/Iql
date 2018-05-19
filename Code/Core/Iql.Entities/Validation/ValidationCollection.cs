using Iql.Entities.Rules;

namespace Iql.Entities.Validation
{

    public class ValidationCollection<TEntity> : RuleCollection<ValidationRule<TEntity>>
    {
    }
}