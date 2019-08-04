using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Entities.Extensions;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;

namespace Iql.Entities.Relationships
{
    [DebuggerDisplay("{Property.Name} - Relationship")]
    public class RelationshipDetail<T, TProperty> :
        RelationshipDetailTypedBase<T, TProperty,
        RelationshipDetail<T, TProperty>>,
        ISourceRelationshipDetail
        where T : class
    {
        public override IProperty PrimaryProperty
        {
            get { return Property; }
        }

        public RelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
            
        }

        public RelationshipDetail<T, TProperty> CreateWithRelationshipValue<TRelationship>(
            Expression<Func<TProperty, TRelationship>> relationship,
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, TRelationship>>>> expression,
            bool useForFiltering = true
        )
        {
            var property = OtherSide.EntityConfiguration.FindNestedPropertyByLambdaExpression(relationship);
            var container = property.Relationship.ThisEnd;
            var mapping = RelationshipMappings.FirstOrDefault(_ => _.Property == container);
            if (mapping == null)
            {
                mapping = new RelationshipMapping(this);
                RelationshipMappings.Add(mapping);
            }
            mapping.Property = container;
            var expressionResult = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, EntityConfiguration.Builder, typeof(RelationshipFilterContext<T>));
            mapping.UseForFiltering = useForFiltering;
            mapping.Expression = expressionResult.Expression as IqlLambdaExpression;
            return this;
        }

        public RelationshipDetail<T, TProperty> CreateWithPropertyValue<TNestedProperty>(
            Expression<Func<TProperty, TNestedProperty>> property,
            Expression<Func<T, TNestedProperty>> expression,
            bool useForFiltering = true
        )
        {
            var container = OtherSide.EntityConfiguration.FindNestedPropertyByLambdaExpression(property);
            var mapping = ValueMappings.FirstOrDefault(_ => _.Property == container);
            if (mapping == null)
            {
                mapping = new ValueMapping(this);
                ValueMappings.Add(mapping);
            }
            mapping.Property = container;
            mapping.UseForFiltering = useForFiltering;
            mapping.Expression = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, EntityConfiguration.Builder, Type).Expression as IqlLambdaExpression;
            return this;
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.RelationshipSource;

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]{};
        }

        protected override IRuleCollection<IRelationshipRule> NewRelationshipFilterRulesCollection()
        {
            return new RelationshipRuleCollection<T, TProperty>();
        }

        protected override IRuleCollection<IDisplayRule> NewDisplayRulesCollection()
        {
            return new DisplayRuleCollection<T>();
        }

        protected override IRuleCollection<IBinaryRule> NewValidationRulesCollection()
        {
            return new ValidationCollection<T>();
        }
    }
}