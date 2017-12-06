using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfiguration<T> : EntityConfigurationBase, IEntityConfiguration where T : class
    {
        private readonly EntityConfigurationBuilder _builder;
        private readonly Dictionary<string, IProperty> _propertiesMap = new Dictionary<string, IProperty>();

        public EntityConfiguration(Type type, EntityConfigurationBuilder builder)
        {
            Type = type;
            _builder = builder;
            Relationships = new List<IRelationship>();
            Properties = new List<IProperty>();
        }

        public List<IRelationship> Relationships { get; set; }

        public RelationshipMatch FindRelationship(string propertyName)
        {
            return AllRelationships().SingleOrDefault(r => r.ThisEnd.Property.PropertyName == propertyName);
        }

        public List<RelationshipMatch> AllRelationships()
        {
            return AllRelationshipsInternal(true);
        }

        public List<RelationshipMatch> AllRelationshipsInternal(bool nested)
        {
            var list = new List<RelationshipMatch>();
            foreach (var relationship in Relationships)
            {
                var ends = new[] { relationship.Source, relationship.Target };
                for (var i = 0; i < ends.Length; i++)
                {
                    if (ends[i].Configuration == this)
                    {
                        var relationshipMatch = new RelationshipMatch(relationship, i == 1);
                        if (relationship.Type == RelationshipType.OneToOne && nested)
                        {
                            //var matches = (List<RelationshipMatch>)GetType().GetMethod(nameof(FindAllRelationships),
                            //    BindingFlags.Instance | BindingFlags.NonPublic)
                            //    .MakeGenericMethod(relationshipMatch.OtherEnd.Type)
                            //    .Invoke(this, new object[]{ relationshipMatch.OtherEnd.Configuration });
                            //var match = matches.SingleOrDefault(m => m.Relationship.Type == RelationshipType.OneToOne &&
                            //                                         m.OtherEnd.Property.PropertyName ==
                            //                                         relationshipMatch.ThisEnd.Property.PropertyName &&
                            //                                         m.ThisEnd.Property.PropertyName ==
                            //                                         relationshipMatch.OtherEnd.Property.PropertyName &&
                            //                                         relationshipMatch.Relationship != relationship);
                            //relationshipMatch.InverseOneToOneRelationship = match;
                            //match.InverseOneToOneRelationship = relationshipMatch;
                        }
                        list.Add(relationshipMatch);
                    }
                }
            }
            return list;
        }

        private List<RelationshipMatch> FindAllRelationships<TRelationship>(EntityConfiguration<TRelationship> configuration) where TRelationship : class
        {
            return configuration.AllRelationshipsInternal(false);
        }

        public bool EntityHasKey(object entity, CompositeKey key)
        {
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                var compositeKeyValue = key.Keys.SingleOrDefault(k => k.Name == id.PropertyName);
                if (compositeKeyValue == null)
                {
                    return false;
                }
                if (!Equals(entity.GetPropertyValue(id.PropertyName), compositeKeyValue.Value))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public bool KeysMatch(object left, object right)
        {
            if (new[] { left, right }.Count(i => i == null) == 1)
            {
                return false;
            }
            if (left.GetType() != right.GetType())
            {
                return false;
            }
            if (left == right)
            {
                return true;
            }
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                if (!Equals(left.GetPropertyValue(id.PropertyName), right.GetPropertyValue(id.PropertyName)))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public CompositeKey GetCompositeKey(object entity)
        {
            var key = new CompositeKey();
            foreach (var property in Key.Properties)
            {
                key.Keys.Add(new KeyValue(property.PropertyName, entity.GetPropertyValue(property.PropertyName), FindProperty(property.PropertyName).Type));
            }
            return key;
        }

        public List<IProperty> Properties { get; set; }
        public IEntityKey Key { get; set; }
        public Type Type { get; set; }

        public IProperty FindProperty(string name)
        {
            return _propertiesMap.ContainsKey(name) ? _propertiesMap[name] : null;
        }

        public IEntityKey GetKey()
        {
            return Key;
        }

        public EntityConfiguration<T> HasKey<TKey>(
            Expression<Func<T, TKey>> property
        )
        {
            Key = new EntityKey<T, TKey>();
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            iql.ReturnType = typeof(TKey).ToIqlType();
            Key.Properties.Add(iql);
            TrySetKey(iql.PropertyName);
            return this;
        }

        private void TrySetKey(string propertyName)
        {
            if (Key.Properties.Any(p => p.PropertyName == propertyName))
            {
                var property = FindProperty(propertyName);
                if (property != null)
                {
                    property.Kind = PropertyKind.Key;
                    property.Relationship = null;
                }
            }
        }

        public EntityConfiguration<T> HasCompositeKey(
            params Expression<Func<T, object>>[] properties
        )
        {
            Key = new EntityKey<T, CompositeKey>();
            foreach (var property in properties)
            {
                var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
                //iql.ReturnType = typeof(T).getpro.ToIqlType();
                Key.Properties.Add(iql);
            }
            return this;
        }

        public EntityConfiguration<T> DefineConvertedProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            string convertedFromType,
            bool nullable = true
        )
        {
#if !TypeScript
            if (typeof(IEnumerable).IsAssignableFrom(typeof(TProperty)) && !
                    typeof(string).IsAssignableFrom(typeof(TProperty)))
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
#endif
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<TProperty> ?? new Property<TProperty>(name, false, typeof(T), convertedFromType, false, null);
            definition.Nullable = nullable;
            if (!Properties.Contains(definition))
            {
                Properties.Add(definition);
                _propertiesMap[name] = definition;
            }
            var relationship = FindRelationship(name);
            if (relationship != null)
            {
                definition.Kind = PropertyKind.Relationship;
                definition.Relationship = relationship;
            }
            TrySetKey(iql.PropertyName);
            return this;
        }

        public EntityConfiguration<T> DefineProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            bool nullable = true
        )
        {
            return DefineConvertedProperty(property, null, nullable);
        }

        public EntityConfiguration<T> DefineCollectionProperty<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<T, long?>> countProperty = null
            )
        {
            var collection = MapProperty<TProperty, IEnumerable<TProperty>>(property, true, false, null);
            if (countProperty != null)
            {
                var countDefinition = MapProperty<long?, long?>(countProperty, false, true, collection);
                countDefinition.Kind = PropertyKind.Count;
            }
            TryAssignRelationshipToPropertyDefinition(collection);
            return this;
        }

        private Property<TProperty> MapProperty<TProperty, TValueType>(
            Expression<Func<T, TValueType>> property,
            bool isCollection,
            bool readOnly,
            IProperty countRelationship)
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<TProperty> ?? new Property<TProperty>(name, isCollection, typeof(T), null, readOnly, countRelationship);
            if (!Properties.Contains(definition))
            {
                Properties.Add(definition);
                _propertiesMap[name] = definition;
            }
            TryAssignRelationshipToPropertyDefinition(definition);
            return definition;
        }

        public OneToRelationshipMap<T, TTarget> HasOne<TTarget>(
            Expression<Func<T, TTarget>> property) where TTarget : class
        {
            return new OneToRelationshipMap<T, TTarget>(
                _builder,
                this,
                _builder.DefineEntity<TTarget>(),
                RelationshipMapType.One,
                property);
        }

        public ManyToRelationshipMap<T, TTarget> HasMany<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> property) where TTarget : class
        {
            return new ManyToRelationshipMap<T, TTarget>(
                _builder,
                this,
                _builder.DefineEntity<TTarget>(),
                RelationshipMapType.Many,
                property);
        }

        internal override void TryAssignRelationshipToProperty(string propertyName, bool tryAssignOtherEnd = true)
        {
            var propertyDefinition = FindProperty(propertyName);
            if (propertyDefinition != null)
            {
                TryAssignRelationshipToPropertyDefinition(propertyDefinition, tryAssignOtherEnd);
            }
        }

        internal override void TryAssignRelationshipToPropertyDefinition(IProperty definition, bool tryAssignOtherEnd = true)
        {
            var relationship = FindRelationship(definition.Name);
            if (relationship != null)
            {
                definition.Kind = PropertyKind.Relationship;
                definition.Relationship = relationship;
                var otherEndConfiguration = _builder.GetEntityByType(relationship.OtherEnd.Type);
                foreach (var constraint in relationship.Relationship.Constraints)
                {
                    var constraintProperty = otherEndConfiguration.FindProperty(constraint.SourceKeyProperty.PropertyName);
                    if (constraintProperty != null && constraintProperty.Kind != PropertyKind.RelationshipKey && constraintProperty.Kind != PropertyKind.Key)
                    {
                        constraintProperty.Kind = PropertyKind.RelationshipKey;
                        constraintProperty.Relationship = otherEndConfiguration.FindRelationship(relationship.OtherEnd.Property.PropertyName);
                    }
                }
                if (tryAssignOtherEnd)
                {
                    (otherEndConfiguration as EntityConfigurationBase)
                        .TryAssignRelationshipToProperty(relationship.OtherEnd.Property.PropertyName, false);
                }
            }
            else
            {
                foreach (var relationshipMatch in AllRelationships())
                {
                    foreach (var constraint in relationshipMatch.Relationship.Constraints)
                    {
                        if (constraint.SourceKeyProperty.PropertyName == definition.Name && definition.Kind != PropertyKind.RelationshipKey && definition.Kind != PropertyKind.Key)
                        {
                            definition.Kind = PropertyKind.RelationshipKey;
                            definition.Relationship = relationshipMatch;
                        }
                    }
                }
            }
        }
    }
}