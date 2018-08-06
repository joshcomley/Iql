using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsEnumOrNullableEnum(this Type type)
        {
            if (type.IsEnum)
            {
                return true;
            }

            var underlying = Nullable.GetUnderlyingType(type);
            return underlying != null && underlying.IsEnum;
        }

        public static bool IsAny(this Type type, params Type[] args)
        {
            return args.Any(t => t == type);
        }

        public static string SimpleName(this Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int backtickIndex = friendlyName.IndexOf('`');
                if (backtickIndex != -1)
                {
                    friendlyName = friendlyName.Remove(backtickIndex);
                }
            }
            return friendlyName;
        }

        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
                return type.GetProperties();

            return (new Type[] { type })
                .Concat(type.GetInterfaces())
                .SelectMany(i => i.GetProperties());
        }


        public static PropertyInfo[] GetPublicProperties2(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                                      | BindingFlags.Public | BindingFlags.Instance);
        }
    }
}