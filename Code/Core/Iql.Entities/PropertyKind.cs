using System;

namespace Iql.Entities
{
    [Flags]
    public enum PropertyKind
    {
        Primitive = 1,
        Relationship = 2 << 0,
        Key = Primitive | 2 << 1,
        RelationshipKey = Primitive | 2 << 2,
        Count = Primitive | 2 << 3
    }
}