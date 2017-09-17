using System;
using System.Linq;

namespace Iql.Extensions
{
    public static class IqlTypeExtensions
    {
        public static IqlType ToIqlType(this Type type)
        {
            if (type == typeof(string))
            {
                return IqlType.String;
            }
            if (type == typeof(bool))
            {
                return IqlType.Boolean;
            }
            if (new[] {typeof(short), typeof(int), typeof(long)}.Contains(type))
            {
                return IqlType.Integer;
            }
            if (new[] {typeof(float), typeof(double)}.Contains(type))
            {
                return IqlType.Decimal;
            }
            return IqlType.Unknown;
        }
    }
}