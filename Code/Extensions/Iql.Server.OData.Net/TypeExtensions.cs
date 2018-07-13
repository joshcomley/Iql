using System;

namespace Iql.Server.OData.Net
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type == typeof(string) || Nullable.GetUnderlyingType(type) != null;
        }
    }
}