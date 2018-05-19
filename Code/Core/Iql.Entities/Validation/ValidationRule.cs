using System;
using System.Linq.Expressions;
using Iql.Data.Configuration.Rules;

namespace Iql.Data.Configuration.Validation
{
    public class ValidationRule<TEntity> : BinaryRule<TEntity>, IValidationRule
    {
        public ValidationRule(Expression<Func<TEntity, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        protected override bool InverseResult => true;
    }
}