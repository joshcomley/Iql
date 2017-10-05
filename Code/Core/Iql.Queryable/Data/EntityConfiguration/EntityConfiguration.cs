using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Extensions;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfiguration<T> : IEntityConfiguration where T : class
    {
        private readonly EntityConfigurationBuilder _builder;
        private readonly Dictionary<string, IKeyProperty> _propertiesMap = new Dictionary<string, IKeyProperty>();

        public EntityConfiguration(Type type, EntityConfigurationBuilder builder)
        {
            Type = type;
            _builder = builder;
            Relationships = new List<IRelationship>();
            Properties = new List<IKeyProperty>();
        }

        public List<IRelationship> Relationships { get; set; }
        public List<IKeyProperty> Properties { get; set; }
        public IEntityKey Key { get; set; }
        public Type Type { get; set; }

        public IKeyProperty GetProperty(string name)
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

        public EntityConfiguration<T> HasCompositeKey<TKey>(
            params Expression<Func<T, object>>[] properties
        )
        {
            Key = new EntityKey<T, TKey>();
            foreach (var property in properties)
            {
                var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
                //iql.ReturnType = typeof(T).getpro.ToIqlType();
                Key.Properties.Add(iql);
            }
            return this;
        }

        public EntityConfiguration<T> DefineProperty<TProperty>(
            Expression<Func<T, TProperty>> property
        )
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(TProperty)) && !
                    typeof(string).IsAssignableFrom(typeof(TProperty)))
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = new KeyProperty<TProperty>(name);
            Properties.Add(definition);
            _propertiesMap[name] = definition;
            return this;
        }

        public EntityConfiguration<T> DefineCollectionProperty<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> properties)
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(properties) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = new KeyProperty<TProperty>(name);
            Properties.Add(definition);
            _propertiesMap[name] = definition;
            return this;
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