using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Data.Configuration.Relationships
{
    public class OneToManyRelationship<TSource, TTarget, TCollection>
        : OneToRelationship<TSource, TTarget, TTarget, TCollection>
        where TSource : class 
        where TTarget : class
        where TCollection : IEnumerable<TSource>
    {
        public OneToManyRelationship(
            EntityConfigurationBuilder configuration, 
            Expression<Func<TSource, TTarget>> sourceProperty, 
            Expression<Func<TTarget, TCollection>> targetProperty)
            : base(
                  configuration, 
                  sourceProperty,
                  typeof(TSource),
                  targetProperty, 
                  typeof(TTarget),
                  RelationshipKind.OneToMany)
        {
        }
    }
}