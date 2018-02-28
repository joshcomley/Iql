using System;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.Data.DataStores.InMemory.QueryApplicator.Applicators
{
    public class IqlReverseOperationApplicator
        : QueryOperationApplicator<ReverseOperation, IqlQueryResult, IqlQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<ReverseOperation, TEntity, IqlQueryResult, IqlQueryableAdapter> context)
        {
            throw new Exception("Reverse is not supported for Iql.");
        }
    }
}