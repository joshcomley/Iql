using System;
using System.Linq.Expressions;
using Iql.Entities.Rules;

namespace Iql.Entities.Validation
{
    public class ValidationRule<TEntity> : BinaryRule<TEntity>, IValidationRule
    {
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        protected override bool InverseResult => true;
    }
}