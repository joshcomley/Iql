using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;

namespace Iql.Entities.Relationships
{
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

        public RelationshipDetail<T, TProperty> CreateWithRelationshipValue<TRelationship>(
            Expression<Func<TProperty, TRelationship>> relationship,
            Expression<Func<T, TRelationship>> expression
        )
        {
            var property = OtherSide.EntityConfiguration.FindNestedPropertyByLambdaExpression(relationship);
            var container = property.Relationship.ThisEnd;
            var mapping = RelationshipMappings.FirstOrDefault(_ => _.Container == container);
            if (mapping == null)
            {
                mapping = new RelationshipMapping();
                RelationshipMappings.Add(mapping);
            }
            mapping.Container = container;
            var expressionResult = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, Type);
            mapping.Expression = expressionResult.Expression;
            return this;
        }

        public RelationshipDetail<T, TProperty> CreateWithPropertyValue<TNestedProperty>(
            Expression<Func<TProperty, TNestedProperty>> property,
            Expression<Func<T, TNestedProperty>> expression
        )
        {
            var container = OtherSide.EntityConfiguration.FindNestedPropertyByLambdaExpression(property);
            var mapping = ValueMappings.FirstOrDefault(_ => _.Container == container);
            if (mapping == null)
            {
                mapping = new ValueMapping();
                ValueMappings.Add(mapping);
            }
            mapping.Container = container;
            mapping.Expression = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, Type).Expression;
            return this;
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