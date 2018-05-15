using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.Rules;

namespace Iql.Queryable.Data.EntityConfiguration.Validation
{
    public class ValidationRule<TEntity> : BinaryRule<TEntity>, IValidationRule
    {
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        protected override bool InverseResult => true;
    }
}