using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public class Rule<TEntity> : IRule
    {
        private readonly Func<object, bool> _ivalidate;

        private readonly Expression<Func<object, bool>> _ivalidationExpression;
        private readonly Func<TEntity, bool> _run;

        public Rule(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            Key = key;
            Message = message;
            Expression = expression;
            _run = expression.Compile();
            _ivalidationExpression = _ => Run((TEntity) _);
            _ivalidate = _ivalidationExpression.Compile();
        }

        public Expression<Func<TEntity, bool>> Expression { get; }
        public Func<TEntity, bool> Run => _ => !_run(_);
        public string Key { get; }
        public string Message { get; }
        Expression<Func<object, bool>> IRule.Expression => _ivalidationExpression;
        Func<object, bool> IRule.Run => _ivalidate;
    }
}