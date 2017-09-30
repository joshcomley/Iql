using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class SkipOperationApplicatorOData : QueryOperationApplicator<SkipOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<SkipOperation, TEntity, IODataQuery> context)
        {
            context.Data.AddQueryPart(ODataQueryPart.Skip, context.Operation.Skip.ToString());
        }
    }
}