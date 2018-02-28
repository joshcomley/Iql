using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class
        ManyToManyRelationship<TPivot, TSource, TTarget> : Relationship<TSource, TTarget, IEnumerable<TTarget>,
            IEnumerable<TSource>>, IManyToManyRelationship
        where TSource : class
        where TTarget : class
        where TPivot : class
    {
        public ManyToManyRelationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, IEnumerable<TTarget>>> sourceProperty,
            Expression<Func<TTarget, IEnumerable<TSource>>> targetProperty,
            Expression<Func<TPivot, object>> pivotSourceKeyProperty,
            Expression<Func<TPivot, object>> pivotTargetKeyProperty
        ) : base(
            configuration,
            sourceProperty,
            typeof(TSource),
            targetProperty,
            typeof(TTarget),
            RelationshipKind.ManyToMany)
        {
            PivotType = typeof(TPivot);
            PivotSourceKeyProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(pivotSourceKeyProperty) as
                    IqlPropertyExpression;
            PivotTargetKeyProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(pivotTargetKeyProperty) as
                    IqlPropertyExpression;
        }

        public IqlPropertyExpression PivotTargetKeyProperty { get; }
        public IqlPropertyExpression PivotSourceKeyProperty { get; }
        public Type PivotType { get; }
    }
}