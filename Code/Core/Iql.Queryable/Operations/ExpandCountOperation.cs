using System.Collections.Generic;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class ExpandCountOperation<T, TTarget, TTargetElement>
        : ExpressionQueryOperation<IqlPropertyExpression, ExpandQueryExpression<T, TTarget, TTargetElement>>, IExpandCountOperation
        where TTarget : class
    {
        public ExpandCountOperation(ExpandQueryExpression<T, TTarget, TTargetElement> queryExpression = null)
        {
            QueryExpression = queryExpression;
        }

        public List<ExpandDetail> ExpandDetails { get; set; } = new List<ExpandDetail>();

        public IQueryableBase ApplyQuery(IQueryableBase queryable)
        {
            return queryable;
        }

        public override QueryExpression GetExpression()
        {
            return QueryExpression;
        }
    }
}