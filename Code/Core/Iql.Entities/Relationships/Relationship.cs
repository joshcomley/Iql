using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipBase : IRelationship
    {
        protected EntityConfigurationBuilder Builder;

        protected RelationshipBase()
        {
            Constraints = new List<IRelationshipConstraint>();
        }

        public void Configure(
            EntityConfigurationBuilder builder,
            Func<IRelationshipDetail> source,
            Func<IRelationshipDetail> target,
            RelationshipKind kind)
        {
            Builder = builder;
            Kind = kind;
            Source = source();
            Target = target();
        }

        public List<IRelationshipConstraint> Constraints { get; private set; }
        public RelationshipKind Kind { get; private set; }
        public IRelationshipDetail Source { get; private set; }
        public IRelationshipDetail Target { get; private set; }
        public string ConstraintKey { get; private set; }
        public string QualifiedConstraintKey { get; private set; }

        protected void UpdateConstraintKey()
        {
            ConstraintKey = string.Join(",", Constraints.Select(c => c.TargetKeyProperty.Name));
            QualifiedConstraintKey = $"{Target.Type.Name}:{ConstraintKey}";
        }
    }
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
                () => new RelationshipDetail<TSource, TSourceProperty>(this, RelationshipSide.Source, configuration, sourceProperty, targetElementType),
                () => new RelationshipDetail<TTarget, TTargetProperty>(this, RelationshipSide.Target, configuration, targetProperty, sourceElementType),
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
                Builder.EntityType<TSource>().FindProperty(sourceIqlProperty.PropertyName),
                Builder.EntityType<TTarget>().FindProperty(targetIqlProperty.PropertyName)));
            UpdateConstraintKey();
            return this;
        }
    }
}