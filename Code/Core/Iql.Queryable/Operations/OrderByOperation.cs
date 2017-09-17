using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class OrderByOperation : ExpressionQueryOperation<IqlExpression, ExpressionQueryExpressionBase>
    {
        private readonly bool _descending;
        private readonly ExpressionQueryExpressionBase _queryExpression;

        public OrderByOperation(ExpressionQueryExpressionBase queryExpression = null, bool descending = false)
        {
            _queryExpression = queryExpression;
            _descending = descending;
        }

        public bool IsDescending()
        {
            return _descending;
        }

        public override QueryExpression GetExpression()
        {
            return _queryExpression;
        }
    }
}