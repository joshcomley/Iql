using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class WithKeyOperationApplicatorOData : QueryOperationApplicator<WithKeyOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WithKeyOperation, TEntity, IODataQuery> context)
        {
            context.Data.Key = context.Operation.Key;
            context.Data.HasKey = true;
        }
    }
}