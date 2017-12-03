using System;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationshipDetail
    {
        RelationshipSide RelationshipSide { get; }
        IRelationship Relationship { get; }
        Type Type { get; }
        bool IsCollection { get; }
        IqlPropertyExpression Property { get; set; }
        IEntityConfiguration Configuration { get; set; }
        CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false);
        IqlPropertyExpression[] Constraints();
    }
}