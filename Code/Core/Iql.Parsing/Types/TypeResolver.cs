using System;

namespace Iql.Parsing.Types
{
    internal class EmptyTypeResolver : ITypeResolver
    {
        public ResolvedType ResolveTypeFromTypeName(string typeName)
        {
            return null;
        }
    }
}