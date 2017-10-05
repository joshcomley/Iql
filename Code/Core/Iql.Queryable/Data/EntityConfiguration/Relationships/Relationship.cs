using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : IRelationship
        where TSource : class where TTarget : class
    {
        public Relationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            RelationshipType type)
        {
            //_configuration = configuration;
            Type = type;
            Constraints = new List<IRelationshipConstraint>();
            Source = new RelationshipDetail<TSource, TSourceProperty>(configuration, sourceProperty);
            Target = new RelationshipDetail<TTarget, TTargetProperty>(configuration, targetProperty);
        }

        public List<IRelationshipConstraint> Constraints { get; }
        public RelationshipType Type { get; set; }
        public IRelationshipDetail Source { get; }
        public IRelationshipDetail Target { get; }

        public Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> WithConstraint(
            Expression<Func<TSource, object>> sourceKeyProperty,
            Expression<Func<TTarget, object>> targetKeyProperty)
        {
            Constraints.Add(new RelationshipConstraint(
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(sourceKeyProperty) as
                    IqlPropertyExpression,
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(targetKeyProperty) as
                    IqlPropertyExpression));
            return this;
        }
    }
}