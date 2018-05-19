using System;
using System.Linq.Expressions;
using Iql.Parsing.Expressions;
using Iql.Parsing.Expressions.QueryExpressions;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Queryable.Expressions
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