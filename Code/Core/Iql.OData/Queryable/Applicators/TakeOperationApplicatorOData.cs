using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class TakeOperationApplicatorOData : QueryOperationApplicator<TakeOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<TakeOperation, TEntity, IODataQuery> context)
        {
            context.Data.AddQueryPart(ODataQueryPart.Take, context.Operation.Take.ToString());
        }
    }
}