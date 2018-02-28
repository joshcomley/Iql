using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.DotNet.QueryableApplicator.Applicators
{
    public class TakeOperationApplicatorDotNet
        : QueryOperationApplicatorDotNet<TakeOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(IQueryOperationContext<TakeOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context, ParameterExpression root, IEnumerable<TEntity> typedList)
        {
            return typedList.Take(context.Operation.Take).ToList();
        }
    }
}