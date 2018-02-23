using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class RelationshipDetail<T, TProperty> : IRelationshipDetail where T : class
    {
        private IProperty[] _constraints;

        public RelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            EntityConfigurationBuilder configuration,
            Expression<Func<T, TProperty>> expression,
            Type elementType)
        {
            Relationship = relationship;
            if (relationship.Kind == RelationshipKind.OneToOne)
            {
                int a = 0;
            }
            RelationshipSide = relationshipSide;
            Configuration = configuration.GetEntity<T>();
            //var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as
            //    IqlPropertyExpression;
            Property = Configuration.FindOrDefineProperty<TProperty>(expression, elementType);
            switch (relationship.Kind)
            {
                case RelationshipKind.ManyToMany:
                    IsCollection = true;
                    break;
                case RelationshipKind.OneToMany:
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

        public void MarkDirty(object entity)
        {
            if (entity != null)
            {
                CompositeKeys.Remove(entity);
                InverseCompositeKeys.Remove(entity);
            }
        }

        public IProperty[] Constraints()
        {
            if (_constraints != null)
            {
                return _constraints;
            }
            switch (RelationshipSide)
            {
                case RelationshipSide.Target:
                    return _constraints = Relationship.Constraints.Select(rc => rc.TargetKeyProperty).ToArray();
                case RelationshipSide.Source:
                    return _constraints = Relationship.Constraints.Select(rc => rc.SourceKeyProperty).ToArray();
            }
            throw new NotSupportedException();
        }

        internal Dictionary<object, CompositeKey> CompositeKeys { get; }
            = new Dictionary<object, CompositeKey>();
        internal Dictionary<object, CompositeKey> InverseCompositeKeys { get; }
            = new Dictionary<object, CompositeKey>();
        public CompositeKey GetCompositeKey(object entity, bool inverse = false)
        {
            var dic = inverse
                ? InverseCompositeKeys
                : CompositeKeys;
            if (dic.ContainsKey(entity))
            {
                return dic[entity];
            }
            return GetCompositeKeyInternal(entity, inverse, dic);
        }

        private CompositeKey GetCompositeKeyInternal(object entity, bool inverse, Dictionary<object, CompositeKey> dic)
        {
            var constraints = Constraints();
            var inverseConstraints = RelationshipSide == RelationshipSide.Source
                ? Relationship.Target.Constraints()
                : Relationship.Source.Constraints();
            var compositeKey = new CompositeKey(constraints.Length);
            compositeKey.Entity = entity;
            for (var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                var value = entity.GetPropertyValue(constraint);
                var keyValue = new KeyValue(
                    inverse
                        ? inverseConstraints[i].Name
                        : constraint.Name,
                    value,
                    constraint.ElementType);
                compositeKey.Keys[i] = keyValue;
            }

            if (!dic.ContainsKey(entity))
            {
                dic.Add(entity, compositeKey);
            }

            return compositeKey;
        }
    }
}