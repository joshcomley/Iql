using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Operations
{
    public class ExpandOperation : ExpressionQueryOperation<IqlPropertyExpression, ExpandQueryExpression>, IExpandOperation
    {
        public ExpandOperation(ExpandQueryExpression queryExpression = null, bool countOnly = false) : base(queryExpression)
        {
            QueryExpression = queryExpression;
            CountOnly = countOnly;
        }

        // public List<ExpandDetail> ExpandDetails { get; set; } = new List<ExpandDetail>();
        public bool CountOnly { get; set; }

        //public IQueryableBase ApplyQuery(IQueryableBase queryable)
        //{
        //    return queryable;
        //}
    }
}