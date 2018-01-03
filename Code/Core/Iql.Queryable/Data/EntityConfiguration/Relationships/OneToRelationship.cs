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
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            RelationshipType type) :
            base(
                configuration,
                sourceProperty,
                targetProperty,
                type)
        {
        }
    }
}