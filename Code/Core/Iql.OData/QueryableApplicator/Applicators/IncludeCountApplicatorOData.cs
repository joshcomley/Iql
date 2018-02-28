using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class IncludeCountApplicatorOData : QueryOperationApplicator<IncludeCountOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            context.Data.IncludeCount = true;
        }
    }
}