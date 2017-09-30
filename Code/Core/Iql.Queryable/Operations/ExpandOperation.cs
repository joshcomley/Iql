using System.Collections.Generic;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public class ExpandOperation<T, TTarget, TTargetElement>
        : ExpressionQueryOperation<IqlPropertyExpression, ExpandQueryExpression<T, TTarget, TTargetElement>>, IExpandOperation
        where TTarget : class
    {
        public ExpandOperation(ExpandQueryExpression<T, TTarget, TTargetElement> queryExpression = null)
        {
            QueryExpression = queryExpression;
        }

        public List<ExpandDetail> ExpandDetails { get; set; } = new List<ExpandDetail>();

        public IQueryableBase ApplyQuery(IQueryableBase queryable)
        {
            return queryable;
            //var abx = typeof(TTarget);
            //return ApplyQuery((IQueryable<TTarget>) queryable);
        }

        //public ExpandQueryExpression<T, TTarget> QueryExpression { get; }

        public override QueryExpression GetExpression()
        {
            return QueryExpression;
        }

        //public IQueryable<TTarget> ApplyQuery(IQueryable<TTarget> queryable)
        //{
        //    return queryable;
        //}
    }
}