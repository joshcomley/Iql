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
            switch (relationship.Type)
            {
                case RelationshipType.ManyToMany:
                    IsCollection = true;
                    break;
                case RelationshipType.OneToMany:
                    IsCollection = relationshipSide == RelationshipSide.Target;
                    break;
            }
        }

        public RelationshipSide RelationshipSide { get; }
        public IRelationship Relationship { get; }
        public Type Type => typeof(T);
        public bool IsCollection { get; }
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

        public CompositeKey GetCompositeKey(object entity, bool inverse = false)
        {
            var constraints = Constraints();
            var inverseConstraints = RelationshipSide == RelationshipSide.Source
                ? Relationship.Target.Constraints()
                : Relationship.Source.Constraints();
            var compositeKey = new CompositeKey();
            for(var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                compositeKey.Keys.Add(new KeyValue(
                    inverse
                        ? inverseConstraints[i].PropertyName
                        : constraint.PropertyName,
                    entity.GetPropertyValue(constraint.PropertyName),
                    Configuration.FindProperty(constraint.PropertyName).Type));
            }
            return compositeKey;
        }
    }
}