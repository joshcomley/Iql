using System;

namespace Iql.Entities.Relationships
{
    public interface IRelationshipDetailMetadata
    {
        RelationshipSide RelationshipSide { get; }
        Type Type { get; }
        bool IsCollection { get; }
        IProperty Property { get; set; }
        IProperty CountProperty { get; }
    }
}