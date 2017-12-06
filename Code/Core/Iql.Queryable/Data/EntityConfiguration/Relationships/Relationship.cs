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
            Source = new RelationshipDetail<TSource, TSourceProperty>(this, RelationshipSide.Source, configuration, sourceProperty);
            Target = new RelationshipDetail<TTarget, TTargetProperty>(this, RelationshipSide.Target, configuration, targetProperty);
        }

        public List<IRelationshipConstraint> Constraints { get; }
        public RelationshipType Type { get; set; }
        public IRelationshipDetail Source { get; }
        public IRelationshipDetail Target { get; }

        public Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> WithConstraint(
            Expression<Func<TSource, object>> sourceKeyProperty,
            Expression<Func<TTarget, object>> targetKeyProperty)
        {
            var sourceIqlProperty = IqlQueryableAdapter.ExpressionToIqlExpressionTree(sourceKeyProperty) as
                IqlPropertyExpression;
            var targetIqlProperty = IqlQueryableAdapter.ExpressionToIqlExpressionTree(targetKeyProperty) as
                IqlPropertyExpression;
            var sourceProperty = Source.Configuration.FindProperty(sourceIqlProperty.PropertyName);
            if (sourceProperty != null && sourceProperty.Kind == PropertyKind.Primitive)
            {
                sourceProperty.Kind = PropertyKind.RelationshipKey;
                sourceProperty.Relationship = Source.Configuration.FindRelationship(Source.Property.PropertyName);
            }
            var targetProperty = Target.Configuration.FindProperty(targetIqlProperty.PropertyName);
            if (targetProperty != null && targetProperty.Kind == PropertyKind.Primitive)
            {
                targetProperty.Kind = PropertyKind.RelationshipKey;
                targetProperty.Relationship = Target.Configuration.FindRelationship(Target.Property.PropertyName);
            }
            Constraints.Add(new RelationshipConstraint(
                sourceIqlProperty,
                targetIqlProperty));
            return this;
        }
    }
}