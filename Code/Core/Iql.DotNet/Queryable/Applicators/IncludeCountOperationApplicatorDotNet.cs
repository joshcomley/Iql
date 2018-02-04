using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class IncludeCountOperationApplicatorDotNet : DotNetQueryOperationApplicator<IncludeCountOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context, ParameterExpression root, IEnumerable<TEntity> typedList)
        {
            //return ExpandOperationApplicatorDotNet.ApplyExpand(
            //    typedList,
            //    context.Operation,
            //    context.Data)
            return typedList;
        }
    }
}