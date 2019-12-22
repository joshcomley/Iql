using System;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.Relationships
{
    public abstract class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : RelationshipBase
        where TSource : class
        where TTarget : class
    {
        protected Relationship(
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
                sourceProperty,
                targetProperty,
                kind);
        }

        public Type SourceElementType { get; }
        public Type TargetElementType { get; }

        public Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> WithConstraint<TKey>(
            Expression<Func<TSource, TKey>> sourceKeyProperty,
            Expression<Func<TTarget, TKey>> targetKeyProperty)
        {
            var expressionConverter = IqlExpressionConversion.DefaultExpressionConverter();
            var sourceIqlProperty = expressionConverter.ConvertPropertyLambdaToIql(sourceKeyProperty, Builder).Expression;
            var targetIqlProperty = expressionConverter.ConvertPropertyLambdaToIql(targetKeyProperty, Builder).Expression;
            var sourceProperty = Source.EntityConfiguration.FindOrDefinePropertyByName(sourceIqlProperty.PropertyName, typeof(TKey));
            if (sourceProperty != null && sourceProperty.Kind.HasFlag(IqlPropertyKind.Primitive))
            {
                sourceProperty.Kind = sourceProperty.Kind | IqlPropertyKind.RelationshipKey;
                sourceProperty.Relationship = Source.EntityConfiguration.FindRelationshipByName(((IMetadata) Source.Property).Name);
            }
            var targetProperty = Target.EntityConfiguration.FindOrDefinePropertyByName(
                targetIqlProperty.PropertyName,
                typeof(TKey));
            if (targetProperty != null && targetProperty.Kind.HasFlag(IqlPropertyKind.Primitive))
            {
                //targetProperty.Kind = targetProperty.Kind | IqlPropertyKind.RelationshipKey;
                targetProperty.RelationshipSources.Add(Target.EntityConfiguration.FindRelationshipByName(((IMetadata) Target.Property).Name));
            }
            Constraints.Add(new RelationshipConstraint(
                Builder.EntityType<TSource>().FindProperty(sourceIqlProperty.PropertyName),
                Builder.EntityType<TTarget>().FindProperty(targetIqlProperty.PropertyName)));
            UpdateConstraintKey();
            return this;
        }
    }
}