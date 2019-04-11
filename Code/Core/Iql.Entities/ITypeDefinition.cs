using System;

namespace Iql.Entities
{
    public interface ITypeDefinition : ITypeConfiguration
    {
        Type ElementType { get; }
        string ElementTypeName { get; }
        Type Type { get; }
        string TypeName { get; }
        Type DeclaringType { get; }
        object EnsureValueType(object value);
    }
}