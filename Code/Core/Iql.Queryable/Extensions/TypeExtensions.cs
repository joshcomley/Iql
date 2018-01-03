using System;
using System.Collections;

namespace Iql.Queryable.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsEnumerable<TProperty>()
        {
            return IsEnumerableType(typeof(TProperty));
        }

        public static bool IsEnumerableType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && !
                       typeof(string).IsAssignableFrom(type);
        }

        public static object DefaultValue(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}