﻿using System;
using System.Collections;
using System.Linq;

namespace Iql.Extensions
{
    public static class IqlTypeExtensions
    {
        public static string GetFullName(this Type type)
        {
            if (type == typeof(int))
            {
                return "int";
            }

            if (type == typeof(short))
            {
                return "short";
            }

            if (type == typeof(byte))
            {
                return "byte";
            }

            if (type == typeof(bool))
            {
                return "bool";
            }

            if (type == typeof(long))
            {
                return "long";
            }

            if (type == typeof(float))
            {
                return "float";
            }

            if (type == typeof(double))
            {
                return "double";
            }

            if (type == typeof(decimal))
            {
                return "decimal";
            }

            if (type == typeof(string))
            {
                return "string";
            }

            if (type.IsGenericType)
            {
                return type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFullName(x)).ToArray()) + ">";
            }

            return type.Name;
        }

        public static object ActivateGeneric(
            this Type type,
            object[] parameters,
            params Type[] types)
        {
            return Activator.CreateInstance(type.MakeGenericType(types), parameters
#if TypeScript
                .Concat(types)
#endif
            );
        }

        public static bool IsEnumerable<TProperty>()
        {
            return IsEnumerableType(typeof(TProperty));
        }

        public static bool IsEnumerableType(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && !
                       typeof(string).IsAssignableFrom(type);
        }

        public static IqlType ToIqlType(this Type type)
        {
            if (type == null)
            {
                return IqlType.Unknown;
            }
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                type = underlyingType;
            }

            if (type.IsEnumerableType())
            {
                return IqlType.Collection;
            }
            if (type == typeof(string))
            {
                return IqlType.String;
            }
            if (type == typeof(bool))
            {
                return IqlType.Boolean;
            }
            if (new[] { typeof(short), typeof(int), typeof(long) }.Contains(type))
            {
                return IqlType.Integer;
            }
            if (new[] { typeof(decimal), typeof(float), typeof(double) }.Contains(type))
            {
                return IqlType.Decimal;
            }
            if (new[] { typeof(DateTime), typeof(DateTimeOffset) }.Contains(type))
            {
                return IqlType.Date;
            }
            return IqlType.Unknown;
        }

        public static Type ToType(this IqlType type)
        {
            if (type == IqlType.String)
            {
                return typeof(string);
            }
            if (type == IqlType.Boolean)
            {
                return typeof(bool);
            }
            if (type == IqlType.Integer)
            {
                return typeof(int);
            }
            if (type == IqlType.Decimal)
            {
                return typeof(float);
            }
            return typeof(object);
        }
    }
}