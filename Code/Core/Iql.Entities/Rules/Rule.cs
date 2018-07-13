using System;
using System.Linq.Expressions;

namespace Iql.Entities.Rules
{
    public abstract class Rule<TContext, TResult> : IRuleBase<TResult>
    {
        protected readonly Func<object, TResult> UntypedInvoker;
        protected readonly LambdaExpression LambdaExpression;
        protected readonly Func<TContext, TResult> TypedInvoker;

        protected Rule(Expression<Func<TContext, TResult>> expression, string key, string message)
        {
            Key = key ?? (++RuleCounter.Count).ToString();
            Message = message;
            Expression = expression;
            TypedInvoker = expression.Compile();
            Expression<Func<object, TResult>> untypedExpression = _ => Run((TContext)_);
            UntypedInvoker = untypedExpression.Compile();
            LambdaExpression = expression;
        }

        public virtual Expression<Func<TContext, TResult>> Expression { get; }
        public abstract Func<TContext, TResult> Run { get; }
        public virtual string Key { get; }
        public virtual string Message { get; }
        LambdaExpression IRule.Expression => LambdaExpression;
        Func<object, TResult> IRuleBase<TResult>.Run => UntypedInvoker;
    }
}