using System;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public interface IRelationshipDetailMetadata : IPropertyGroup
    {
        bool AllowInlineEditing { get; set; }
        RelationshipSide RelationshipSide { get; }
        Type Type { get; }
        bool IsCollection { get; }
        IProperty Property { get; set; }
        IProperty CountProperty { get; }
    }
}