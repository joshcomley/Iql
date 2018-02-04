using System;
using System.Collections;

namespace Iql.Queryable.Extensions
{
    public static class TypeExtensions
    {
#if !TypeScript
        public static Type GetListType(this IList list)
        {
            var type = list.GetType();
            while (type.Name != "List`1")
            {
                type = type.BaseType;
            }
            return type;
        }
#endif

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
#if TypeScript
            return Activator.CreateInstance(type);
#else
            return typeof(TypeExtensions).GetMethod(nameof(GetDefaultValue))
                .MakeGenericMethod(type)
                .Invoke(null, null);
#endif
        }

#if !TypeScript
        public static object GetDefaultValue<T>()
        {
            return default(T);
        }
#endif
    }
}