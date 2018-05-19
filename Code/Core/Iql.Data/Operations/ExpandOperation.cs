using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Operations
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