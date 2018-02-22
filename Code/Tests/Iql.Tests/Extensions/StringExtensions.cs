using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Extensions
{
    public static class StringExtensions
    {
        public static string CompressJson(this string json)
        {
#if TypeScript
            return json;
#else
            return JObject.Parse(json).ToString(Formatting.None);
#endif
        }
    }
}