using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Native;
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
                var targetQuery = target.ToQueryWithAdapterBase(
                    new DotNetQueryableAdapter(), 
                    context.DataContext,
                    context,
                    context.Data);
                //var targetQueryable = expand.ApplyQuery()
                var targetData = targetQuery.ToList();
                var targetList = targetData;
                var sourceList = (IList) typedList;
                if (detail.IsTarget)
                {
                    var temp = targetList;
                    targetList = sourceList;
                    sourceList = temp;
                }
                switch (detail.Relationship.Type)
                {
                    case RelationshipType.OneToOne:
                        sourceList.ExpandOneToOne(
                            targetList,
                            detail.Relationship);
                        break;
                    case RelationshipType.OneToMany:
                        sourceList.ExpandOneToMany(
                            targetList,
                            detail.Relationship);
                        break;
                    //case RelationshipType.ManyToMany:
                    //    var manyToMany = detail.Relationship as IManyToManyRelationship;
                    //    typedList.ExpandManyToMany(
                    //        sourceType,
                    //        targetType,
                    //        targetList,
                    //        dotNetQueryResult.GetDataSetObjectName(manyToMany.PivotType),
                    //        detail.IsTarget
                    //            ? manyToMany.PivotTargetKeyProperty.PropertyName
                    //            : manyToMany.PivotSourceKeyProperty.PropertyName,
                    //        detail.IsTarget
                    //            ? manyToMany.PivotSourceKeyProperty.PropertyName
                    //            : manyToMany.PivotTargetKeyProperty.PropertyName,
                    //        thisEnd.Property.Name,
                    //        otherEnd.Property.Name,
                    //        thisConstraint.Name,
                    //        otherConstraint.Name);
                    //    break;
                }
            }
            return typedList;
        }
    }
}