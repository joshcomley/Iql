using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.JavaScript.Extensions
{
    public static class IqlStringExtensions
    {
        public static string Clean(this string str)
        {
            return str.Replace("\r", "");
        }

        public static string CompressJson(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JObject.Load(reader);
            return jObject.ToString(Formatting.None).Clean();
#endif
        }
    }
}