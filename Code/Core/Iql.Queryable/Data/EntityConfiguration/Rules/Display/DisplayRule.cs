using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.Validation;

namespace Iql.Queryable.Data.EntityConfiguration.Rules.Display
{
    public class DisplayRule<TEntity> : Rule<TEntity>, IDisplayRule
    {
        public DisplayRule(Expression<Func<TEntity, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        public DisplayRuleKind Kind { get; set; }
    }
}