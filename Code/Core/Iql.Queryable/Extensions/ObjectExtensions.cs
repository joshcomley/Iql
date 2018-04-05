using System;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Extensions
{
    public static class ObjectExtensions
    {
        public static PropertyInfo[] GetProperties(this object value)
        {
            return value == null
                ? new PropertyInfo[] { }
                : value.GetType().GetProperties().ToArray();
        }

        public static bool IsDefaultValue(this object value, ITypeDefinition type)
        {
            return Equals(value, null) ||
                   Equals(type.DefaultValue(), value);
        }
    }
}