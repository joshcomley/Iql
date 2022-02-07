using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Iql.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Iql.JavaScript.Extensions
{
    public static class IqlStringExtensions
    {
        public static string Clean(this string str)
        {
            str = str.Replace("\r", "");
#if !TypeScript
            str = str.Replace("\"Key\":null,", "");
#endif
            //str = Regex.Replace(str, @"\d\d\d\d-\d\d-\d\dT\d\d:\d\d:\d\d\.\d*\+\d\d:\d\d",
            //    @"0001-01-01T00:00:00.0+00:00");
            return str;
        }

#if !TypeScript
        private static JObject SortPropertiesAlphabetically(JObject original)
        {
            var result = new JObject();

            foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
            {
                var value = property.Value as JObject;

                if (value != null)
                {
                    value = SortPropertiesAlphabetically(value);
                    result.Add(property.Name, value);
                }
                else
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }
#endif

        public static string NormalizeJson(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString().Clean();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
#if TEST
            jObject = SortPropertiesAlphabetically(jObject);
#endif
            return jObject.ToString(Formatting.None, new NumberConverter()).Clean();
#endif
        }

        public static string NormalizeJsonNoNulls(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString().Clean();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
            var newJson = IqlJsonSerializer.Serialize(jObject, new IqlJsonSerializerSettings
            {
                IgnoreNulls = true,
                UseNumberConverter = true
            }).Clean();
            return newJson;
#endif
        }
    }
}