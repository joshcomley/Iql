using System;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class ExpressionQueryOperation<TIqlExpression, TQueryExpression> :
        QueryOperation, IExpressionQueryOperation
        where TIqlExpression : IqlExpression
        where TQueryExpression : ExpressionQueryExpressionBase
    {
        public TIqlExpression Expression { get; set; }

        public TQueryExpression QueryExpression { get; set; }

        IqlExpression IExpressionQueryOperation.Expression
        {
            get => Expression;
            set => Expression = (TIqlExpression) value;
        }

        public virtual QueryExpression GetExpression()
        {
            throw new Exception("Method not implemented.");
        }
    }
}