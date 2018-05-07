using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public abstract class Rule<TEntity> : IRule
    {
        private readonly Func<object, bool> _untypedInvoker;
        protected abstract bool InverseResult { get; }
        private readonly LambdaExpression _lambdaExpression;
        private readonly Func<TEntity, bool> _typedInvoker;

        public Rule(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            Key = key;
            Message = message;
            Expression = expression;
            _typedInvoker = expression.Compile();
            Expression<Func<object, bool>> untypedExpression = _ => Run((TEntity)_);
            _untypedInvoker = untypedExpression.Compile();
            _lambdaExpression = expression;
        }

        public Expression<Func<TEntity, bool>> Expression { get; }
        public Func<TEntity, bool> Run => _ => InverseResult ? !_typedInvoker(_) : _typedInvoker(_);
        public string Key { get; }
        public string Message { get; }
        LambdaExpression IRule.Expression => _lambdaExpression;
        Func<object, bool> IRule.Run => _untypedInvoker;
    }
}