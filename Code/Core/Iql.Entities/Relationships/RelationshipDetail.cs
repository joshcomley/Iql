using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Extensions;

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
            var entityConfiguration = configuration.EntityType<T>();
            Property = entityConfiguration.FindOrDefineProperty<TProperty>(expression, elementType, elementType.ToIqlType());
        }

        public TConfigurable Configure(Action<TConfigurable> action)
        {
            action(this as TConfigurable);
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
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
        }
    }
}