using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IProperty
    {
        bool Nullable { get; set; }
        RelationshipMatch Relationship { get; set; }
        PropertyKind Kind { get; set; }
        IProperty CountRelationship { get; }
        bool ReadOnly { get; }
        string Name { get; }
        Type Type { get; }
        bool IsCollection { get; }
        Type DeclaringType { get; }
        string ConvertedFromType { get; }
    }
}