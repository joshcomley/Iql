using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Expressions;
using Iql.Queryable.Extensions;
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
            RelationshipMatches matches = null;
            var relationshipExpander = context.Data.GetRoot().RelationshipExpander;
            WithRelationships(
                context,
                context.Operation.Expression,
                (path, relationship) =>
                {
                    matches = relationshipExpander.FindMatches(
                        context.Data.GetRoot().DataSetByType(path.Property.Relationship.OtherEnd.Type),
                        (IList)typedList,
                        path.Property.Relationship.Relationship,
                        true);
                });
            var result = typedList.Where((Func<TEntity, bool>)method).ToList();
            if (matches != null)
            {
                matches.UnassignRelationships();
            }
            return result;
        }
    }
}