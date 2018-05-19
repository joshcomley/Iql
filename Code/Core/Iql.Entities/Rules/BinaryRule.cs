using System;
using System.Linq.Expressions;

namespace Iql.Entities.Rules
{
    public abstract class BinaryRule<TContext> : Rule<TContext, bool>, IBinaryRule
    {
        protected abstract bool InverseResult { get; }

        protected BinaryRule(Expression<Func<TContext, bool>> expression, string key, string message) : base(expression, key, message)
        {
        }

        public override Func<TContext, bool> Run => _ => InverseResult ? !TypedInvoker(_) : TypedInvoker(_);
    }
}