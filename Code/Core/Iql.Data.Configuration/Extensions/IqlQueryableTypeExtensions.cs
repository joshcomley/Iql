using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql.Data.Configuration.Extensions
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

        private static readonly Dictionary<Type, object> DefaultValues = new Dictionary<Type, object>();
        public static object DefaultValue(this ITypeDefinition type)
        {
#if TypeScript
            if (type.ConvertedFromType == nameof(Guid))
            {
                return Guid.Empty;
            }
            if (type.Kind == IqlType.Enum) 
            {
                return 0;
            }
            if(type.Type == typeof(DateTime))
            {
                return new DateTime();
            }
            return Activator.CreateInstance(type.Type);
#else
            if (DefaultValues.ContainsKey(type.Type))
            {
                return DefaultValues[type.Type];
            }
            var defaultValue = typeof(IqlQueryableTypeExtensions)
                .GetMethod(nameof(GetDefaultValue))
                .InvokeGeneric(null, null, type.Type);
            DefaultValues.Add(type.Type, defaultValue);
            return defaultValue;
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