using System;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public abstract class OneToRelationship<TSource, TTarget, TSourceProperty, TTargetProperty>
        : Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> 
        where TSource : class 
        where TTarget : class
    {
        protected OneToRelationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Type sourceElementType,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            Type targetElementType,
            RelationshipKind kind) :
            base(
                configuration,
                sourceProperty,
                sourceElementType,
                targetProperty,
                targetElementType,
                kind)
        {
        }
    }
}