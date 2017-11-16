using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Extensions;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfiguration<T> : IEntityConfiguration where T : class
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
            foreach (var relationship in Relationships)
            {
                var ends = new[] {relationship.Source, relationship.Target};
                for (var i = 0; i < ends.Length; i++)
                {
                    if (ends[i].Configuration == this && ends[i].Property.PropertyName == propertyName)
                    {
                        return new RelationshipMatch(relationship, i == 0);
                    }
                }
            }
            return null;
        }
        public List<IProperty> Properties { get; set; }
        public IEntityKey Key { get; set; }
        public Type Type { get; set; }

        public IProperty GetProperty(string name)
        {
            return _propertiesMap[name];
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
            return this;
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
            string convertedFromType
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
            var definition = new Property<TProperty>(name, false, typeof(T), convertedFromType);
            Properties.Add(definition);
            _propertiesMap[name] = definition;
            return this;
        }

        public EntityConfiguration<T> DefineProperty<TProperty>(
            Expression<Func<T, TProperty>> property
        )
        {
            return DefineConvertedProperty(property, null);
        }

        public EntityConfiguration<T> DefineCollectionProperty<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<T, long?>> countProperty = null
            )
        {
            var collection = MapProperty<TProperty, IEnumerable<TProperty>>(property, true, false, null);
            if (countProperty != null)
            {
                MapProperty<long?, long?>(countProperty, false, true, collection);
            }
            return this;
        }

        private Property<TProperty> MapProperty<TProperty, TValueType>(Expression<Func<T, TValueType>> property, bool isCollection, bool readOnly, IProperty countRelationship)
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = new Property<TProperty>(name, isCollection, typeof(T), null, readOnly, countRelationship);
            Properties.Add(definition);
            _propertiesMap[name] = definition;
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
    }
}