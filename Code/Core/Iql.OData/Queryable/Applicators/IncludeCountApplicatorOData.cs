using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class IncludeCountApplicatorOData : QueryOperationApplicator<IncludeCountOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            context.Data.IncludeCount = true;
        }
    }
}