using Newtonsoft.Json;

namespace Iql.Conversion
{
    public static class JsonSerializableExtensions
    {

        public static string ToJson(this IJsonSerializable obj)
        {
            return JsonConvert.SerializeObject(obj.PrepareForJson(), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}