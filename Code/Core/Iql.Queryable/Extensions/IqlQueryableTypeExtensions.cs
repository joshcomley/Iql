using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql.Queryable.Extensions
{
    public static class IqlQueryableTypeExtensions
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

        public static string SimpleName(this Type type)
        {
            var name = type.Name;
            var index = name.IndexOf("`");
            if (index != -1)
            {
                return name.Substring(0, index);
            }

            return name;
        }

        private static readonly Dictionary<Type, object> DefaultValues = new Dictionary<Type,object>();
        public static object DefaultValue(this Type type)
        {
            if (DefaultValues.ContainsKey(type))
            {
                return DefaultValues[type];
            }
#if TypeScript
            var defaultValue = Activator.CreateInstance(type);
#else
            var defaultValue = typeof(IqlQueryableTypeExtensions)
                .GetMethod(nameof(GetDefaultValue))
                .InvokeGeneric(null, null, type);
#endif
            DefaultValues.Add(type, defaultValue);
            return defaultValue;
        }

#if !TypeScript
        public static object GetDefaultValue<T>()
        {
            return default(T);
        }
#endif
    }
}