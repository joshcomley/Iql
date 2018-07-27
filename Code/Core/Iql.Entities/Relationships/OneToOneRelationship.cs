using System;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
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

        protected override IRelationshipDetail BuildSource(LambdaExpression property)
        {
            return new RelationshipDetail<TSource, TTarget>(this, RelationshipSide.Source, Builder,
                property, TargetElementType);
        }

        protected override IRelationshipDetail BuildTarget(LambdaExpression property)
        {
            return new RelationshipDetail<TTarget, TSource>(this, RelationshipSide.Target, Builder,
                property, SourceElementType);
        }
    }
}