using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class OneToRelationshipMap<TSource, TTarget> where TSource : class where TTarget : class
    {
        private readonly EntityConfigurationBuilder _configuration;
        private readonly Expression<Func<TSource, TTarget>> _property;
        private readonly EntityConfiguration<TSource> _sourceEntityConfiguration;
        private readonly EntityConfiguration<TTarget> _targetEntityConfiguration;
        private RelationshipMapType _type;

        public OneToRelationshipMap(
            EntityConfigurationBuilder configuration,
            EntityConfiguration<TSource> sourceEntityConfiguration,
            EntityConfiguration<TTarget> targetEntityConfiguration,
            RelationshipMapType type,
            Expression<Func<TSource, TTarget>> property)
        {
            _configuration = configuration;
            _sourceEntityConfiguration = sourceEntityConfiguration;
            _targetEntityConfiguration = targetEntityConfiguration;
            _type = type;
            _property = property;
        }

        public OneToOneRelationship<TSource, TTarget> WithOne(
            Expression<Func<TTarget, TSource>> property
        )
        {
            var relationship = new OneToOneRelationship<TSource, TTarget>(
                _configuration,
                _property,
                property
            );
            _sourceEntityConfiguration.Relationships.Add(relationship);
            if (!Equals(_sourceEntityConfiguration, _targetEntityConfiguration))
            {
                _targetEntityConfiguration.Relationships.Add(relationship);
            }
            _sourceEntityConfiguration.TryAssignRelationshipToPropertyDefinition(relationship.Target.Property.PropertyName);
            return relationship;
        }

        public OneToManyRelationship<TSource, TTarget> WithMany(
            Expression<Func<TTarget, IEnumerable<TSource>>> property)
        {
            var relationship = new OneToManyRelationship<TSource, TTarget>(
                _configuration,
                _property,
                property
            );
            _sourceEntityConfiguration.Relationships.Add(relationship);
            if (!Equals(_sourceEntityConfiguration, _targetEntityConfiguration))
            {
                _targetEntityConfiguration.Relationships.Add(relationship);
            }
            _sourceEntityConfiguration.TryAssignRelationshipToPropertyDefinition(relationship.Target.Property.PropertyName);
            return relationship;
        }
    }
}