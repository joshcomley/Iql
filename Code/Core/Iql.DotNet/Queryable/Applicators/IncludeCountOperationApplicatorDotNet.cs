using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class IncludeCountOperationApplicatorDotNet : DotNetQueryOperationApplicator<IncludeCountOperation>
    {
        public override void Apply<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IDotNetQueryResult> context)
        {
        }

        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(IQueryOperationContext<IncludeCountOperation, TEntity, IDotNetQueryResult> context, ParameterExpression root, IEnumerable<TEntity> typedList)
        {
            return typedList;
        }
    }
}