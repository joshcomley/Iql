using System;

namespace Iql.Queryable.Extensions
{
    public static class TypeExtensions
    {
        public static object DefaultValue(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}