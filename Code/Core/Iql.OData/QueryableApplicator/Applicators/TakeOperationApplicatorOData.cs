using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class TakeOperationApplicatorOData : QueryOperationApplicator<TakeOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<TakeOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            context.Data.TotalTake += context.Operation.Take;
            context.Data.SetQueryPart(ODataQueryPart.Take, context.Data.TotalTake.ToString());
        }
    }
}