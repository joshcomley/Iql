using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class WhereOperationApplicatorDotNet : DotNetQueryOperationApplicator<WhereOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IDotNetQueryResult> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            var lambda = DotNetQueryableAdapter.GetExpression(context.Operation, true,
                context.DataContext.EntityConfigurationContext,
                typeof(TEntity));
            var method = lambda.Compile();
            return typedList.Where((Func<TEntity, bool>) method);
        }
    }
}