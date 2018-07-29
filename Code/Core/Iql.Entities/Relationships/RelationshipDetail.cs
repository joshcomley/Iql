using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipDetailTypedBase<T, TProperty, TConfigurable> : RelationshipDetailBase,
            IConfigurable<TConfigurable>
        //, IConfigurable<RelationshipDetail<T, TProperty>>
        where T : class
        where TConfigurable : RelationshipDetailTypedBase<T, TProperty, TConfigurable>
    {
        protected RelationshipDetailTypedBase(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship,
            relationshipSide)
        {
            EntityConfiguration = configuration.EntityType<T>();
            Property = EntityConfiguration.FindOrDefineProperty<TProperty>(expression, elementType);
        }

        public TConfigurable Configure(Action<TConfigurable> action)
        {
            action(this as TConfigurable);
            return this as TConfigurable;
        }

        public TConfigurable IsInferredWith(Expression<Func<T, TProperty>> expression)
        {
            InferredWith = expression;
            return this as TConfigurable;
        }
    }

    public class RelationshipDetail<T, TProperty> :
        RelationshipDetailTypedBase<T, TProperty,
        RelationshipDetail<T, TProperty>>
        where T : class
    {
        public RelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
        }
    }

    public class CollectionRelationshipDetail<T, TProperty>
        : RelationshipDetailTypedBase<T, IEnumerable<TProperty>, CollectionRelationshipDetail<T, TProperty>>
        where T : class
    {
        public CollectionRelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            EntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
        }
    }
}