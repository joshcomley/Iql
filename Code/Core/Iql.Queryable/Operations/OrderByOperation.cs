using Iql.Parsing.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class OrderByOperation : ExpressionQueryOperation<IqlExpression, QueryExpression>
    {
        private readonly bool _descending;

        public OrderByOperation(QueryExpression queryExpression = null, bool descending = false)
        : base(queryExpression)
        {
            _descending = descending;
        }

        public bool IsDescending()
        {
            return _descending;
        }
    }
}