using System.Collections.Generic;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class ExpandOperation : ExpressionQueryOperation<IqlPropertyExpression, ExpandQueryExpression>, IExpandOperation
    {
        public ExpandOperation(ExpandQueryExpression queryExpression = null) : base(queryExpression)
        {
            QueryExpression = queryExpression;
        }

        public List<ExpandDetail> ExpandDetails { get; set; } = new List<ExpandDetail>();

        public IQueryableBase ApplyQuery(IQueryableBase queryable)
        {
            return queryable;
        }
    }
}