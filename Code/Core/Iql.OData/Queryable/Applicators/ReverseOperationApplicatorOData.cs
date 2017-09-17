using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.OData.Queryable.Applicators
{
    public class ReverseOperationApplicatorOData : QueryOperationApplicator<ReverseOperation, IODataQuery>
    {
        public override void Apply<TEntity>(IQueryOperationContext<ReverseOperation, TEntity, IODataQuery> context)
        {
            throw new NotImplementedException("Reverse is not supported for OData.");
        }
    }
}