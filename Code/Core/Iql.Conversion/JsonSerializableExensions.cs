using Iql.Serialization;
using Newtonsoft.Json;

namespace Iql.Conversion
{
    public static class JsonSerializableExtensions
    {
        public static string ToJson(this IJsonSerializable obj)
        {
            return IqlJsonSerializer.Serialize(obj.PrepareForJson(), new IqlJsonSerializerSettings
            {
                IgnoreNulls = true
            });
        }
    }
}