using System;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationship
    {
        RelationshipType Type { get; }
        Type SourceType { get; }
        Type TargetType { get; }
        IEntityConfiguration SourceConfiguration { get; }
        IqlPropertyExpression SourceProperty { get; }
        IqlPropertyExpression SourceKeyProperty { get; }
        IEntityConfiguration TargetConfiguration { get; }
        IqlPropertyExpression TargetProperty { get; }
        IqlPropertyExpression TargetKeyProperty { get; }
    }
}