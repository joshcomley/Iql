using System;

namespace Iql.Parsing.Types
{
    public class ResolvedType
    {
        public Type Type { get; set; }
        public ResolvedType[] GenericTypeParameters { get; set; }

        public ResolvedType(Type type, ResolvedType[] genericTypeParameters = null)
        {
            Type = type;
            GenericTypeParameters = genericTypeParameters ?? new ResolvedType[] { };
        }
    }
}