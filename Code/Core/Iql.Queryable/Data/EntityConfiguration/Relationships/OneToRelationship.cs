using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
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
            RelationshipType type) :
            base(
                configuration,
                sourceProperty,
                sourceElementType,
                targetProperty,
                targetElementType,
                type)
        {
        }
    }
}