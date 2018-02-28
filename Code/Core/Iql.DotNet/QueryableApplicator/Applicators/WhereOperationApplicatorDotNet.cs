using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.DotNet.QueryableApplicator.Applicators
{
    public class WhereOperationApplicatorDotNet : QueryOperationApplicatorDotNet<WhereOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<WhereOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            var lambda = DotNetQueryableAdapter.GetExpression(context.Operation, true,
                context.DataContext.EntityConfigurationContext,
                typeof(TEntity));
            var method = lambda.Compile();
            var result = typedList.Where((Func<TEntity, bool>) method).ToList();
            return result;
        }
    }
}