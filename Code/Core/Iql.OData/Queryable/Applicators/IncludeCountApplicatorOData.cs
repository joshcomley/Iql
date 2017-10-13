using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class IncludeCountApplicatorOData : QueryOperationApplicator<IncludeCountOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IODataQuery> context)
        {
            context.Data.IncludeCount = true;
        }
    }
}