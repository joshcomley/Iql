using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.Rules;

namespace Iql.Queryable.Data.EntityConfiguration.Validation
{

    public class ValidationCollection<TEntity> : RuleCollection<TEntity, ValidationRule<TEntity>>
    {
        protected override ValidationRule<TEntity> NewRule(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            return new ValidationRule<TEntity>(expression, key, message);
        }
    }
}