using System;
using System.Linq.Expressions;

namespace Iql.Data.Configuration.Relationships
{
    public class OneToOneRelationship<TSource, TTarget> : OneToRelationship<TSource, TTarget, TTarget, TSource>
        where TSource : class where TTarget : class
    {
        public OneToOneRelationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TTarget>> sourceProperty,
            Expression<Func<TTarget, TSource>> targetProperty
        ) : base(
            configuration,
            sourceProperty,
            typeof(TSource),
            targetProperty,
            typeof(TTarget),
            RelationshipKind.OneToOne)
        {
        }
    }
}