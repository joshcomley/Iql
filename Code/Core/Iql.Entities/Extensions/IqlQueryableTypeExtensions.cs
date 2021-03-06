using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Extensions;

namespace Iql.Entities.Extensions
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

        private static bool DefaultValuesDelayedInitialized;
        private static Dictionary<Type, object> DefaultValuesDelayed;
        private static Dictionary<Type, object> DefaultValues { get { if(!DefaultValuesDelayedInitialized) { DefaultValuesDelayedInitialized = true; DefaultValuesDelayed = new Dictionary<Type, object>(); } return DefaultValuesDelayed; } set { DefaultValuesDelayedInitialized = true; DefaultValuesDelayed = value; } }
        public static object DefaultValue(this ITypeDefinition type, bool ignoreNullable = false)
        {            
#if TypeScript
            if (type.Nullable && !ignoreNullable)
            {
                return null;
            }
            if (type.ConvertedFromType == KnownPrimitiveTypes.Guid)
            {
                return Guid.Empty;
            }
            if (type.Kind == IqlType.Enum || type.Kind == IqlType.Integer || type.Kind == IqlType.Decimal)
            {
                return 0;
            }
            if (type.Type == typeof(int))
            {
                return 0;
            }
            if (type.Type == typeof(DateTime))
            {
                return new DateTime();
            }
            if (type.Type == typeof(bool))
            {
                return false;
            }
            return Activator.CreateInstance(type.Type);
#else
            if (DefaultValues.ContainsKey(type.Type))
            {
                return DefaultValues[type.Type];
            }

            var typeToUse = type.Type;
            if (ignoreNullable)
            {
                typeToUse = Nullable.GetUnderlyingType(typeToUse) ?? typeToUse;
            }
            var defaultValue = typeof(IqlQueryableTypeExtensions)
                .GetMethod(nameof(GetDefaultValue))
                .InvokeGeneric(null, null, typeToUse);
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