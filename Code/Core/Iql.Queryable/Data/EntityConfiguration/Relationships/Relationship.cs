using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : IRelationship
        where TSource : class where TTarget : class
    {
        private readonly EntityConfigurationBuilder _configuration;

        public Relationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            RelationshipType type)
        {
            _configuration = configuration;
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
            ;
            var sourceProperty = Source.Configuration.FindOrDefinePropertyByName(sourceIqlProperty.PropertyName);
            if (sourceProperty != null && sourceProperty.Kind == PropertyKind.Primitive)
            {
                sourceProperty.Kind = PropertyKind.RelationshipKey;
                sourceProperty.Relationship = Source.Configuration.FindRelationship(Source.Property.Name);
            }
            var targetProperty = Target.Configuration.FindOrDefinePropertyByName(targetIqlProperty.PropertyName);
            if (targetProperty != null && targetProperty.Kind == PropertyKind.Primitive)
            {
                targetProperty.Kind = PropertyKind.RelationshipKey;
                targetProperty.Relationship = Target.Configuration.FindRelationship(Target.Property.Name);
            }
            Constraints.Add(new RelationshipConstraint(
                _configuration.EntityType<TSource>().FindProperty(sourceIqlProperty.PropertyName),
                _configuration.EntityType<TTarget>().FindProperty(targetIqlProperty.PropertyName)));
            return this;
        }
    }
}