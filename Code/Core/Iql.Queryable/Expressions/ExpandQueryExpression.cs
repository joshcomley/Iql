using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.Queryable;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public class ExpandQueryExpression
        : ExpressionQueryExpression, IExpandQueryExpression
    {
        private Func<IQueryableBase, IQueryableBase> _queryable;

        public ExpandQueryExpression(
            LambdaExpression expression,
            Func<IQueryableBase, IQueryableBase> queryable = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            : base(expression, QueryExpressionKind.NonBinary
#if TypeScript
                  , evaluateContext
#endif
                  )
        {
            Queryable = queryable;
        }

        public Func<IQueryableBase, IQueryableBase> Queryable
        {
            get { return _queryable ?? (_queryable = q => q); }
            set => _queryable = value;
        }
    }
}