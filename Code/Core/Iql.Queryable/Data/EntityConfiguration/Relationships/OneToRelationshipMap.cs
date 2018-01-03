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
            //relationship.Type == RelationshipType.OneToOne
            var sourceRelationshipMatch = _sourceEntityConfiguration.FindRelationship(relationship.Source.Property.Name);
            if (sourceRelationshipMatch == null)
            {
                _sourceEntityConfiguration.Relationships.Add(relationship);
            }
            var targetRelationshipMatch = _targetEntityConfiguration.FindRelationship(relationship.Target.Property.Name);
            if (!Equals(_sourceEntityConfiguration, _targetEntityConfiguration) &&
                targetRelationshipMatch == null)
            {
                _targetEntityConfiguration.Relationships.Add(relationship);
            }
            _sourceEntityConfiguration.TryAssignRelationshipToProperty(relationship.Source.Property.Name);
            return relationship;
        }

        public OneToManyRelationship<TSource, TTarget, TCollection> WithMany<TCollection>(
            Expression<Func<TTarget, TCollection>> property) 
            where TCollection : IEnumerable<TSource>
        {
            var relationship = new OneToManyRelationship<TSource, TTarget, TCollection>(
                _configuration,
                _property,
                property
            );
            if (_sourceEntityConfiguration.FindRelationship(relationship.Source.Property.Name) == null)
            {
                _sourceEntityConfiguration.Relationships.Add(relationship);
            }
            if (!Equals(_sourceEntityConfiguration, _targetEntityConfiguration) &&
                _targetEntityConfiguration.FindRelationship(relationship.Target.Property.Name) == null)
            {
                _targetEntityConfiguration.Relationships.Add(relationship);
            }
            _sourceEntityConfiguration.TryAssignRelationshipToProperty(relationship.Source.Property.Name);
            //_targetEntityConfiguration.TryAssignRelationshipToProperty(relationship.Target.Property.PropertyName);
            return relationship;
        }
    }
}