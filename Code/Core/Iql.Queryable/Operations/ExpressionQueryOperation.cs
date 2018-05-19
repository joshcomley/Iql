using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class ExpressionQueryOperation<TIqlExpression, TQueryExpression> :
        QueryOperation, IExpressionQueryOperation
        where TIqlExpression : IqlExpression
        where TQueryExpression : QueryExpression
    {
        public TIqlExpression Expression { get; set; }

        public TQueryExpression QueryExpression { get; set; }

        IqlExpression IExpressionQueryOperation.Expression
        {
            get => Expression;
            set => Expression = (TIqlExpression) value;
        }

        QueryExpression IExpressionQueryOperation.QueryExpression
        {
            get => QueryExpression;
            set => QueryExpression = (TQueryExpression) value;
        }

        public ExpressionQueryOperation(TQueryExpression queryExpression)
        {
            QueryExpression = queryExpression;
        }
    }
}