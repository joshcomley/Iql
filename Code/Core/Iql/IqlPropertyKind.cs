using System;

namespace Iql
{
    [Flags]
    public enum IqlPropertyKind
    {
        Property = 1,
        Primitive = Property | 2,
        Relationship = Property | 4 << 0,
        Key = Primitive | 4 << 1,
        RelationshipKey = Primitive | 4 << 2,
        Count = Primitive | 4 << 3,
        SimpleCollection = 8 << 0,
        GroupCollection = 8 << 1
    }
}