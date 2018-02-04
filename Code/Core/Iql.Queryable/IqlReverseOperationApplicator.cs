using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
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