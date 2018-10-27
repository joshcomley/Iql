using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Extensions
{
    public static class IqlTypeExtensions
    {
        public static bool IsGeographic(this IqlType type)
        {
            switch (type)
            {
                case IqlType.GeometryLine:
                case IqlType.GeographyLine:
                case IqlType.GeometryMultiLine:
                case IqlType.GeographyMultiLine:
                case IqlType.GeometryPolygon:
                case IqlType.GeographyPolygon:
                case IqlType.GeometryMultiPolygon:
                case IqlType.GeographyMultiPolygon:
                case IqlType.GeometryPoint:
                case IqlType.GeographyPoint:
                case IqlType.GeometryMultiPoint:
                case IqlType.GeographyMultiPoint:
                    return true;
            }

            return false;
        }

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

#if !TypeScript
            if (type.IsGenericType)
            {
                return type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(_ => GetFullName(_)).ToArray()) + ">";
            }
#endif

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

#if !TypeScript
            if (type == typeof(Guid))
            {
                return IqlType.Guid;
            }
#endif
            if (type == typeof(IqlPointExpression))
            {
                return IqlType.GeographyPoint;
            }
            if (type == typeof(IqlMultiPointExpression))
            {
                return IqlType.GeographyMultiPoint;
            }
            if (type == typeof(IqlPolygonExpression))
            {
                return IqlType.GeographyPoint;
            }
            if (type == typeof(IqlMultiPolygonExpression))
            {
                return IqlType.GeographyMultiPolygon;
            }
            if (type == typeof(IqlLineExpression))
            {
                return IqlType.GeographyLine;
            }
            if (type == typeof(IqlMultiLineExpression))
            {
                return IqlType.GeographyMultiLine;
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
            if (type == IqlType.Date)
            {
                return typeof(DateTime);
            }
            if (type == IqlType.Collection)
            {
                return typeof(List<>);
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