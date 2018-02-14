using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class ExpandOperationApplicatorDotNet
        : DotNetQueryOperationApplicator<IExpandOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            var expand = context.Operation;
            var dotNetQueryResult = context.Data;
            return ApplyExpand(context, typedList, expand, dotNetQueryResult);
        }

        internal static IEnumerable<TEntity> ApplyExpand<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IDotNetQueryResult, DotNetQueryableAdapter> context,
            IEnumerable<TEntity> typedList, 
            IExpandOperation expand,
            IDotNetQueryResult dotNetQueryResult)
            where TEntity : class
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var j = 0; j < expand.ExpandDetails.Count; j++)
            {
                var detail = expand.ExpandDetails[j];
                var otherEnd = detail.IsTarget 
                    ? detail.Relationship.Source 
                    : detail.Relationship.Target;
                var targetType = otherEnd.Type;
                // TODO: Support multiple constraints
                //var targetData = dotNetQueryResult.DataSetByType(targetType);
                var targetExpression = expand.GetExpression() as IExpandQueryExpression;
                var targetQueryable = targetExpression.GetQueryable();
                var target = targetQueryable(context.DataContext.AsDbSetByType(targetType));
                var targetQuery = (IDotNetQueryResult)target.ToQueryWithAdapterBase(
                    new DotNetQueryableAdapter(), 
                    context.DataContext,
                    context,
                    context.Data);
                //var targetQueryable = expand.ApplyQuery()
                var targetData = targetQuery.GetResults().Root;
                var targetList = targetData;
                var sourceList = (IList) typedList;

                //context.Data.GetRoot()
                var rootResult = context.Data.GetRoot();
                var relationshipExpander = rootResult
                    .RelationshipExpander;
                if (detail.IsTarget)
                {
                    var matched = relationshipExpander
                        .FindMatches(
                            targetList,
                            sourceList,
                            detail.Relationship,
                            false);
                    rootResult.AddMatches(
                        detail.Relationship.Source.Type,
                        matched.SourceMatches);
                }
                else
                {
                    var matched = relationshipExpander
                        .FindMatches(
                            sourceList,
                            targetList,
                            detail.Relationship,
                            false);
                    rootResult.AddMatches(
                        detail.Relationship.Target.Type,
                        matched.TargetMatches);
                }
            }
            return typedList;
        }
    }
}