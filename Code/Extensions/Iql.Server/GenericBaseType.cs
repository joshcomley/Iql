using System;

namespace Iql.Server
{
    public class GenericBaseType
    {
        public Type Type { get; }
        public Type TypeDefinition { get; }
        public GenericBaseType(Type type, Type typeDefinition)
        {
            Type = type;
            TypeDefinition = typeDefinition;
        }
    }
}