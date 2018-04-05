using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Extensions
{
    public static class StringExtensions
    {
        public static string CompressJson(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JObject.Load(reader);
            return jObject.ToString(Formatting.None);
#endif
        }
    }
}