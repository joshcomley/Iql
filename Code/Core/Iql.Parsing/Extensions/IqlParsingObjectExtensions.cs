using Newtonsoft.Json.Linq;

namespace Iql.Parsing.Extensions
{
    public static class IqlParsingObjectExtensions
    {
        public static T NullPropagate<T>(this object source, string propertyName)
        {
            if (source == null)
            {
                return default(T);
            }

            if (source is JToken)
            {
                return (source as JToken)[propertyName].Value<T>();
            }
            return (T)source.GetType().GetProperty(propertyName).GetValue(source);
        }
    }
}