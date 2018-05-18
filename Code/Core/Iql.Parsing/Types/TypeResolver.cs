using System;

namespace Iql.Parsing.Types
{
    internal class EmptyTypeResolver : ITypeResolver
    {
        public Type ResolveTypeFromTypeName(string typeName)
        {
            return null;
        }
    }
}