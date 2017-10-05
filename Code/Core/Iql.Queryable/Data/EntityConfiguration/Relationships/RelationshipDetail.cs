using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class RelationshipDetail<T, TProperty> : IRelationshipDetail where T : class
    {
        public RelationshipDetail(
            EntityConfigurationBuilder configuration,
            Expression<Func<T, TProperty>> expression)
        {
            Configuration = configuration.GetEntity<T>();
            Property =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as
                    IqlPropertyExpression;

        }

        public Type Type => typeof(T);
        public IqlPropertyExpression Property { get; set; }
        public IqlPropertyExpression KeyProperty { get; set; }
        public IEntityConfiguration Configuration { get; set; }
    }
}