using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.Validation;

namespace Iql.Queryable.Data.EntityConfiguration.Rules.Display
{

    public class DisplayRuleCollection<TEntity> : RuleCollection<TEntity, DisplayRule<TEntity>>
    {
        protected override DisplayRule<TEntity> NewRule(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            return new DisplayRule<TEntity>(expression, key, message);
        }
    }
}