using System;
using System.Collections.Generic;
using Iql.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Server.Serialization.Deserialization.Converters
{
    public class IqlExpressionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jobj = serializer.Deserialize(reader) as JObject;
                var value = IqlJsonDeserializer.DeserializeJson(jobj.ToString());
                return value;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IqlExpression).IsAssignableFrom(objectType);
        }
    }
}