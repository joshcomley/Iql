using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.JavaScript.QueryToJavaScript;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.DotNet.Queryable.Applicators
{
    public class ExpandOperationApplicatorDotNet
        : DotNetQueryOperationApplicator<IExpandOperation>
    {
        protected override IEnumerable<TEntity> ApplyTyped<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IDotNetQueryResult> context,
            ParameterExpression root,
            IEnumerable<TEntity> typedList)
        {
            var expand = context.Operation;
            for (var j = 0; j < expand.ExpandDetails.Count; j++)
            {
                var detail = expand.ExpandDetails[j];
                var thisEnd = detail.IsTarget ? detail.Relationship.Target : detail.Relationship.Source;
                var otherEnd = detail.IsTarget ? detail.Relationship.Source : detail.Relationship.Target;
                var sourceType = thisEnd.Type;
                var targetType = otherEnd.Type;
                // TODO: Support multiple constraints
                var constraint = detail.Relationship.Constraints.First();
                var thisConstraint = detail.IsTarget ? constraint.TargetKeyProperty : constraint.SourceKeyProperty;
                var otherConstraint = detail.IsTarget ? constraint.SourceKeyProperty : constraint.TargetKeyProperty;
                var targetList = context.Data.DataSetByType(targetType);
                var sourceList = (IList)typedList;
                if (detail.IsTarget)
                {
                    var temp = targetList;
                    targetList = sourceList;
                    sourceList = temp;
                }
                switch (detail.Relationship.Type)
                {
                    case RelationshipType.OneToOne:
                        typedList.ExpandOneToOne(
                            context.Data.DataSetByType(targetType),
                            thisEnd.Property.PropertyName,
                            otherEnd.Property.PropertyName,
                            thisConstraint.PropertyName,
                            otherConstraint.PropertyName);
                        break;
                    case RelationshipType.OneToMany:
                        sourceList.ExpandOneToMany(
                            detail.Relationship.Source.Type,
                            targetList,
                            detail.Relationship.Source.Property.PropertyName,
                            detail.Relationship.Target.Property.PropertyName,
                            constraint.SourceKeyProperty.PropertyName,
                            constraint.TargetKeyProperty.PropertyName);
                        break;
                    case RelationshipType.ManyToMany:
                        var manyToMany = detail.Relationship as IManyToManyRelationship;
                        typedList.ExpandManyToMany(
                            sourceType,
                            targetType,
                            context.Data.DataSetByType(targetType),
                            context.Data.GetDataSetObjectName(manyToMany.PivotType),
                            detail.IsTarget ? manyToMany.PivotTargetKeyProperty.PropertyName : manyToMany.PivotSourceKeyProperty.PropertyName,
                            detail.IsTarget ? manyToMany.PivotSourceKeyProperty.PropertyName : manyToMany.PivotTargetKeyProperty.PropertyName,
                            thisEnd.Property.PropertyName,
                            otherEnd.Property.PropertyName,
                            thisConstraint.PropertyName,
                            otherConstraint.PropertyName);
                        break;
                }
            }
            return typedList;
        }
    }
}