using System;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class ReverseOperationApplicatorOData : QueryOperationApplicator<ReverseOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<ReverseOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            throw new NotImplementedException("Reverse is not supported for OData.");
        }
    }
}