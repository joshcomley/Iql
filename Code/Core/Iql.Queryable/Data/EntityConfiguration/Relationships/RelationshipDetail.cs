using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class RelationshipDetail<T, TProperty> : IRelationshipDetail where T : class
    {
        public RelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            EntityConfigurationBuilder configuration,
            Expression<Func<T, TProperty>> expression)
        {
            Relationship = relationship;
            RelationshipSide = relationshipSide;
            Configuration = configuration.GetEntity<T>();
            Property =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as
                    IqlPropertyExpression;

        }

        public RelationshipSide RelationshipSide { get; }
        public IRelationship Relationship { get; }
        public Type Type => typeof(T);
        public IqlPropertyExpression Property { get; set; }
        public IqlPropertyExpression KeyProperty { get; set; }
        public IEntityConfiguration Configuration { get; set; }

        public IqlPropertyExpression[] Constraints()
        {
            switch (RelationshipSide)
            {
                case RelationshipSide.Target:
                    return Relationship.Constraints.Select(rc => rc.TargetKeyProperty).ToArray();
                case RelationshipSide.Source:
                    return Relationship.Constraints.Select(rc => rc.SourceKeyProperty).ToArray();
            }
            throw new NotSupportedException();
        }

        public CompositeKey GetCompositeKey(object entity)
        {
            var constraints = Constraints();
            var compositeKey = new CompositeKey();
            foreach (var constraint in constraints)
            {
                compositeKey.Keys.Add(new KeyValue(constraint.PropertyName, entity.GetPropertyValue(constraint.PropertyName)));
            }
            return compositeKey;
        }
    }
}