using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class ManyToRelationshipMap<TSource, TTarget> where TSource : class where TTarget : class
    {
        private readonly EntityConfigurationBuilder _configuration;
        private readonly Expression<Func<TSource, IEnumerable<TTarget>>> _property;
        private readonly EntityConfiguration<TSource> _sourceEntityConfiguration;
        private readonly EntityConfiguration<TTarget> _targetEntityConfiguration;
        private RelationshipMapType _type;

        public ManyToRelationshipMap(
            EntityConfigurationBuilder configuration,
            EntityConfiguration<TSource> sourceEntityConfiguration,
            EntityConfiguration<TTarget> targetEntityConfiguration,
            RelationshipMapType type,
            Expression<Func<TSource, IEnumerable<TTarget>>> property)
        {
            _configuration = configuration;
            _sourceEntityConfiguration = sourceEntityConfiguration;
            _targetEntityConfiguration = targetEntityConfiguration;
            _type = type;
            _property = property;
        }

        public ManyToManyRelationship<TPivot, TSource, TTarget> WithMany<TPivot>(
            Expression<Func<TTarget, IEnumerable<TSource>>> property,
            Expression<Func<TPivot, object>> sourceKeyProperty,
            Expression<Func<TPivot, object>> targetKeyProperty
        ) where TPivot : class
        {
            var relationship = new ManyToManyRelationship<TPivot, TSource, TTarget>(
                _configuration,
                _property,
                property,
                sourceKeyProperty,
                targetKeyProperty
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