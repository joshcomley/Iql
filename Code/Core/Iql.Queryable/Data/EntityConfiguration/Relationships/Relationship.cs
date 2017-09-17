using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class Relationship<TSource, TTarget, TSourceProperty, TTargetProperty> : IRelationship
        where TSource : class where TTarget : class
    {
        private readonly EntityConfigurationBuilder _configuration;

        public Relationship(
            EntityConfigurationBuilder configuration,
            Expression<Func<TSource, TSourceProperty>> sourceProperty,
            Expression<Func<TTarget, TTargetProperty>> targetProperty,
            RelationshipType type)
        {
            //_configuration = configuration;
            Type = type;
            SourceConfiguration = configuration.GetEntity<TSource>();
            TargetConfiguration = configuration.GetEntity<TTarget>();
            SourceProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(sourceProperty) as
                    IqlPropertyExpression;
            TargetProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(targetProperty) as
                    IqlPropertyExpression;
        }

        public IEntityConfiguration TargetConfiguration { get; set; }
        public IEntityConfiguration SourceConfiguration { get; set; }
        public IqlPropertyExpression SourceProperty { get; set; }
        public IqlPropertyExpression TargetProperty { get; set; }
        public IqlPropertyExpression SourceKeyProperty { get; set; }
        public IqlPropertyExpression TargetKeyProperty { get; set; }
        public Type SourceType => typeof(TSource);
        public Type TargetType => typeof(TTarget);
        public RelationshipType Type { get; set; }

        public void WithKey(
            Expression<Func<TSource, object>> sourceKeyProperty,
            Expression<Func<TTarget, object>> targetKeyProperty)
        {
            SourceKeyProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(sourceKeyProperty) as
                    IqlPropertyExpression;
            TargetKeyProperty =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(targetKeyProperty) as
                    IqlPropertyExpression;
        }
    }
}