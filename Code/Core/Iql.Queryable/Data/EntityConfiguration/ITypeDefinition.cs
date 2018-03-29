using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface ITypeDefinition
    {
        string ConvertedFromType { get; }
        bool Nullable { get; }
        Type ElementType { get; }
        Type Type { get; }
        bool IsCollection { get; }
        Type DeclaringType { get; }
        IqlType Kind { get; }
        object EnsureValueType(object value);
    }
}