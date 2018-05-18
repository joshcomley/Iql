using System;

namespace Iql.Parsing.Types
{
    public interface ITypeResolver
    {
        Type ResolveTypeFromTypeName(string typeName);
    }
}