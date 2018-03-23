using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class OrderByOperationApplicatorOData : QueryOperationApplicator<OrderByOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<OrderByOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            var orderBy =
                ODataQueryableAdapter.GetExpression(context.Operation, context.DataContext.EntityConfigurationContext);
            if (context.Operation.IsDescending())
            {
                orderBy = orderBy + " desc";
            }
            context.Data.AddQueryPart(ODataQueryPart.OrderBy, orderBy);
        }
    }
}