using System;

namespace Iql.Queryable.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDefaultValue(this object value)
        {
            return Equals(value, null) ||
                   Equals(value, 0) ||
                   Equals(value, "") ||
                   Equals(value, new DateTime());
        }
    }
}