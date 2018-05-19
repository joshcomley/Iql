using Iql.Data.Configuration.Rules;

namespace Iql.Data.Configuration.Validation
{

    public class ValidationCollection<TEntity> : RuleCollection<ValidationRule<TEntity>>
    {
    }
}