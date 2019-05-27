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

#if !TypeScript
            if (source is JToken)
            {
                var jToken = (source as JToken)[propertyName];
                return jToken == null ? default(T) : jToken.Value<T>();
            }
#endif
            return (T)source.GetType().GetProperty(propertyName).GetValue(source);
        }
    }
}