using System;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.Relationships
{
    public class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : RelationshipBase
        where TSource : class
        where TTarget : class
    {
        public Relationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Type sourceElementType,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            Type targetElementType,
            RelationshipKind kind)
        {
            SourceElementType = sourceElementType;
            TargetElementType = targetElementType;
            Configure(configuration,
                () => new RelationshipDetail<TSource, TSourceProperty, TTarget>(this, RelationshipSide.Source, configuration, sourceProperty, targetElementType),
                () => new RelationshipDetail<TTarget, TTargetProperty, TSource>(this, RelationshipSide.Target, configuration, targetProperty, sourceElementType),
                kind);
        }

        public Type SourceElementType { get; }
        public Type TargetElementType { get; }

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
                sourceProperty.Relationship = Source.Configuration.FindRelationshipByName(Source.Property.Name);
            }
            var targetProperty = Target.Configuration.FindOrDefinePropertyByName(
                targetIqlProperty.PropertyName,
                typeof(TKey));
            if (targetProperty != null && targetProperty.Kind.HasFlag(PropertyKind.Primitive))
            {
                //targetProperty.Kind = targetProperty.Kind | PropertyKind.RelationshipKey;
                targetProperty.RelationshipSources.Add(Target.Configuration.FindRelationshipByName(Target.Property.Name));
            }
            Constraints.Add(new RelationshipConstraint(
                Builder.EntityType<TSource>().FindProperty(sourceIqlProperty.PropertyName),
                Builder.EntityType<TTarget>().FindProperty(targetIqlProperty.PropertyName)));
            UpdateConstraintKey();
            return this;
        }
    }
}