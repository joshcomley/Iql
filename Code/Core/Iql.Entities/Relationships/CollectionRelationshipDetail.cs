using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public class CollectionRelationshipDetail<T, TProperty>
        : RelationshipDetailTypedBase<T, IEnumerable<TProperty>, CollectionRelationshipDetail<T, TProperty>>
        where T : class
    {
        public CollectionRelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
        }
    }
}