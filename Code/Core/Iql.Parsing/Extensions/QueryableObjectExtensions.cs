using System.Linq;
using System.Reflection;

namespace Iql.Parsing.Extensions
{
    public static class QueryableObjectExtensions
    {
        public static bool HasPropertyValue(this object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetRuntimeProperties()
                .SingleOrDefault(p => p.Name == propertyName);
            if (property == null)
            {
                return false;
            }
            var objValue = property.GetValue(obj);
            return Equals(objValue, value);
        }
    }
}