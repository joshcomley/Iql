using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class OneToManyRelationship<TSource, TTarget>
        : OneToRelationship<TSource, TTarget, TTarget, IEnumerable<TSource>> where TSource : class where TTarget : class
    {
        public OneToManyRelationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TTarget>> sourceProperty,
            Expression<Func<TTarget, IEnumerable<TSource>>> targetProperty
        )
            : base(
                configuration,
                sourceProperty,
                targetProperty,
                RelationshipType.OneToMany)
        {
        }
    }
}