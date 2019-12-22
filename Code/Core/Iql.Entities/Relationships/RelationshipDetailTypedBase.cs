using System;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Extensions;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipDetailTypedBase<T, TProperty, TConfigurable> : RelationshipDetailBase,
            IConfigurable<TConfigurable>
        //, IConfigurable<RelationshipDetail<T, TProperty>>
        where T : class
        where TConfigurable : RelationshipDetailTypedBase<T, TProperty, TConfigurable>
    {
        protected override string ResolveName()
        {
            return ((IMetadata) Property)?.Name;
        }

        protected RelationshipDetailTypedBase(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship,
            relationshipSide)
        {
            var entityConfiguration = configuration.EntityType<T>();
            var property = IqlConverter.Instance.ConvertPropertyLambdaExpressionToIql<T>(expression, configuration).Expression;
            Property = entityConfiguration.FindOrDefineProperty<TProperty>(property.PropertyName, elementType, elementType.ToIqlType());
        }

        public TConfigurable Configure(Action<TConfigurable> action)
        {
            action(this as TConfigurable);
            return this as TConfigurable;
        }
    }
}