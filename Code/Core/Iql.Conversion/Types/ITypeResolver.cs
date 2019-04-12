using System;
using Iql.Entities;

namespace Iql.Parsing.Types
{
    public interface ITypeResolver
    {
        IIqlTypeMetadata FindType<T>();
        IIqlTypeMetadata FindTypeByType(Type type);
        IIqlTypeMetadata ResolveTypeFromTypeName(string typeName);
        IIqlTypeMetadata GetTypeMap(IIqlTypeMetadata type);
    }
}