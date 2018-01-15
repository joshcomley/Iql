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
            Expression<Func<T, TProperty>> expression,
            Type elementType)
        {
            Relationship = relationship;
            RelationshipSide = relationshipSide;
            Configuration = configuration.GetEntity<T>();
            //var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as
            //    IqlPropertyExpression;
            Property = Configuration.FindOrDefineProperty<TProperty>(expression, elementType);
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
        public IProperty Property { get; set; }
        public IProperty KeyProperty { get; set; }
        public IEntityConfiguration Configuration { get; set; }

        public IProperty[] Constraints(bool inverse = false)
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

        public CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false)
        {
            var constraints = Constraints(inverse);
            var inverseConstraints = RelationshipSide == RelationshipSide.Source
                ? Relationship.Target.Constraints()
                : Relationship.Source.Constraints();
            var compositeKey = new CompositeKey();
            compositeKey.Entity = entityOrCompositeKey;
            for(var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                object value;
                if (entityOrCompositeKey is CompositeKey)
                {
                    value = (entityOrCompositeKey as CompositeKey).Keys.Single(k => k.Name == constraint.Name).Value;
                }
                else
                {
                    value = entityOrCompositeKey.GetPropertyValue(constraint);
                }
                compositeKey.Keys.Add(new KeyValue(
                    inverse
                        ? inverseConstraints[i].Name
                        : constraint.Name,
                    value,
                    constraint.ElementType));
            }
            return compositeKey;
        }
    }
}