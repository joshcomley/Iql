using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public class IqlReverseOperationApplicator
        : QueryOperationApplicator<ReverseOperation, IqlQueryResult>
    {
        public override void Apply<TEntity>(IQueryOperationContext<ReverseOperation, TEntity, IqlQueryResult> context)
        {
            throw new Exception("Reverse is not supported for Iql.");
        }
    }
}