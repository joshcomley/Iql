using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class ExpandOperationApplicatorOData : QueryOperationApplicator<IExpandOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IExpandOperation, TEntity, IODataQuery> context)
        {
            context.Data.Expands.Add(context.Operation);
        }
    }
}