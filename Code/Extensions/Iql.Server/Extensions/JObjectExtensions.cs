using Iql.Entities;
using Newtonsoft.Json.Linq;

namespace Iql.Server.Extensions
{
    public static class JObjectExtensions
    {
        public static bool HasProperty(this JToken postedValues, string property)
        {
            if (postedValues == null)
            {
                return false;
            }

            if (!(postedValues is JObject))
            {
                return false;
            }

            return (postedValues as JObject).ContainsKey(property);
        }

        public static JToken GetPropertyValue(this JToken postedValues, string property)
        {
            if (!(postedValues is JObject))
            {
                return false;
            }

            return (postedValues as JObject)[property];
        }

        public static bool HasPostedValue(this JToken postedValues, IqlPropertyPath path)
        {
            var curr = postedValues;
            foreach (var part in path.PropertyPath)
            {
                if (!(curr is JObject))
                {
                    return false;
                }
                if (!(curr as JObject).ContainsKey(part.PropertyName))
                {
                    return false;
                }

                curr = postedValues[part.PropertyName];
            }
            return true;
        }

        public static bool HasPostedKey(this JObject postedValues, IqlPropertyPath path)
        {
            JToken curr = postedValues;
            foreach (var part in path.PropertyPath)
            {
                if (!(curr is JObject))
                {
                    return false;
                }
                if (!(curr as JObject).ContainsKey(part.PropertyName))
                {
                    return false;
                }

                curr = postedValues[part.PropertyName];
            }
            return true;
        }
    }
}