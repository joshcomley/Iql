using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.Relationships
{
    public class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : IRelationship
        where TSource : class where TTarget : class
    {
        private readonly EntityConfigurationBuilder _configuration;

        public Relationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Type sourceElementType,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            Type targetElementType,
            RelationshipKind kind)
        {
            _configuration = configuration;
            SourceElementType = sourceElementType;
            TargetElementType = targetElementType;
            Kind = kind;
            Constraints = new List<IRelationshipConstraint>();
            Source = new RelationshipDetail<TSource, TSourceProperty>(this, RelationshipSide.Source, configuration, sourceProperty, targetElementType);
            Target = new RelationshipDetail<TTarget, TTargetProperty>(this, RelationshipSide.Target, configuration, targetProperty, sourceElementType);
        }

        public List<IRelationshipConstraint> Constraints { get; }
        public Type SourceElementType { get; }
        public Type TargetElementType { get; }
        public RelationshipKind Kind { get; set; }
        public IRelationshipDetail Source { get; }
        public IRelationshipDetail Target { get; }
        public string ConstraintKey { get; private set; }
        public string QualifiedConstraintKey { get; private set; }

        public Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> WithConstraint<TKey>(
            Expression<Func<TSource, TKey>> sourceKeyProperty,
            Expression<Func<TTarget, TKey>> targetKeyProperty)
        {
            var expressionConverter = IqlExpressionConversion.DefaultExpressionConverter();
            var sourceIqlProperty = expressionConverter.ConvertPropertyLambdaToIql(sourceKeyProperty).Expression;
            var targetIqlProperty = expressionConverter.ConvertPropertyLambdaToIql(targetKeyProperty).Expression;
            var sourceProperty = Source.Configuration.FindOrDefinePropertyByName(sourceIqlProperty.PropertyName, typeof(TKey));
            if (sourceProperty != null && sourceProperty.Kind.HasFlag(PropertyKind.Primitive))
            {
                sourceProperty.Kind = sourceProperty.Kind | PropertyKind.RelationshipKey;
                sourceProperty.Relationship = Source.Configuration.FindRelationship(Source.Property.Name);
            }
            var targetProperty = Target.Configuration.FindOrDefinePropertyByName(
                targetIqlProperty.PropertyName,
                typeof(TKey));
            if (targetProperty != null && targetProperty.Kind.HasFlag(PropertyKind.Primitive))
            {
                //targetProperty.Kind = targetProperty.Kind | PropertyKind.RelationshipKey;
                targetProperty.RelationshipSources.Add(Target.Configuration.FindRelationship(Target.Property.Name));
            }
            Constraints.Add(new RelationshipConstraint(
                _configuration.EntityType<TSource>().FindProperty(sourceIqlProperty.PropertyName),
                _configuration.EntityType<TTarget>().FindProperty(targetIqlProperty.PropertyName)));
            UpdateConstraintKey();
            return this;
        }

        private void UpdateConstraintKey()
        {
            ConstraintKey = string.Join(",", Constraints.Select(c => c.TargetKeyProperty.Name));
            QualifiedConstraintKey = $"{Target.Type.Name}:{ConstraintKey}";
        }
    }
}