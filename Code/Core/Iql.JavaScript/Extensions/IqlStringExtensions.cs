using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TypeSharp.Extensions;

namespace Iql.JavaScript.Extensions
{
    [DoNotConvert]
    class NumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double?) || objectType == typeof(float) || objectType == typeof(float?) || objectType == typeof(decimal) || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return Decimal.Parse(token.ToString(),
                    System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                 token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (Equals(value, null))
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteRawValue(value.ToString());
            }
        }
    }
    public static class IqlStringExtensions
    {

        public static string Clean(this string str)
        {
            return str.Replace("\r", "");
        }

        public static string NormalizeJson(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
            return jObject.ToString(Formatting.None, new NumberConverter()).Clean();
#endif
        }

        public static string NormalizeJsonNoNulls(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
            var newJson = JsonConvert.SerializeObject(jObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new[] { new NumberConverter() }
            }).Clean();
            return newJson;
#endif
        }
    }
}