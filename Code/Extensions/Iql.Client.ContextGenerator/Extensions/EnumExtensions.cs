using System;
using System.Linq;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class EnumExtensions
    {
        public static string ToCodeString<TEnum>(this TEnum value)
            where TEnum : struct
        {
            return
                $"{string.Join(" | ", value.ToString().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(e => $"{typeof(TEnum).Name}.{e.Trim()}"))}";
        }

        public static bool IsValid<TEnum>(this TEnum enumValue)
            where TEnum : struct
        {
            return IsValidEnumValue(enumValue);
        }

        public static bool IsValidEnumValue(object enumValue)
        {
            if (enumValue == null)
            {
                return false;
            }
            var firstChar = enumValue.ToString()[0];
            return (firstChar < '0' || firstChar > '9') && firstChar != '-';
        }
    }
}