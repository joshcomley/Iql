using System;

namespace Iql.Entities
{
    public interface ITypeDefinition : ITypeConfiguration
    {
        Type ElementType { get; }
        Type Type { get; }
        Type DeclaringType { get; }
        object EnsureValueType(object value);
    }
}