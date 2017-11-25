using System;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationshipDetail
    {
        RelationshipSide RelationshipSide { get; }
        IRelationship Relationship { get; }
        Type Type { get; }
        IqlPropertyExpression Property { get; set; }
        IEntityConfiguration Configuration { get; set; }
        CompositeKey GetCompositeKey(object entity);
        IqlPropertyExpression[] Constraints();
    }
}