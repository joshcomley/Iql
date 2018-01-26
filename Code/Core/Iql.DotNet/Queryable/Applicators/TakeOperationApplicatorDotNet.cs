using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class TakeOperationApplicatorDotNet
        : DotNetQueryOperationApplicator<TakeOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(IQueryOperationContext<TakeOperation, TEntity, IDotNetQueryResult> context, ParameterExpression root, IEnumerable<TEntity> typedList)
        {
            return typedList.Take(context.Operation.Take).ToList();
        }
    }
}