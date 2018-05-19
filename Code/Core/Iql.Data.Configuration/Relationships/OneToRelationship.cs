using System;
using System.Linq.Expressions;

namespace Iql.Data.Configuration.Relationships
{
    public class OneToRelationship<TSource, TTarget, TSourceProperty, TTargetProperty>
        : Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> 
        where TSource : class 
        where TTarget : class
    {
        public OneToRelationship(
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