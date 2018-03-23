using System;
using System.Linq;
using System.Reflection;

namespace Iql.Queryable.Extensions
{
    public static class ObjectExtensions
    {
        public static PropertyInfo[] GetProperties(this object value)
        {
            return value == null 
                ? new PropertyInfo[]{} 
                : value.GetType().GetProperties().ToArray();
        }

        public static bool IsDefaultValue(this object value)
        {
            return Equals(value, null) ||
                   Equals(value, 0) ||
                   Equals(value, "") ||
                   Equals(value, new DateTime());
        }
    }
}