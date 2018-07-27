using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
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

        protected override IRelationshipDetail BuildSource(LambdaExpression property)
        {
            return new RelationshipDetail<TSource, TTarget>(this, RelationshipSide.Source, Builder,
                property, TargetElementType);
        }

        protected override IRelationshipDetail BuildTarget(LambdaExpression property)
        {
            return new CollectionRelationshipDetail<TTarget, TSource>(this, RelationshipSide.Target, Builder,
                property, SourceElementType);
        }
    }
}