using System;
using Iql.Extensions;
using Iql.Server.Serialization.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Server.Serialization.Deserialization.Converters
{
    public class TypeConverter : JsonConverter
    {
        public SerializerDetails Details { get; }

        public TypeConverter(SerializerDetails details)
        {
            Details = details;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var path = reader.Path;
            var jobj = serializer.Deserialize(reader);
            if (Enum.TryParse<IqlType>(jobj as string, true, out var iqlType))
            {
                return iqlType.ToType();
            }
            if(jobj != null)
            {
                //Details.Finalisers.Add(_ =>
                //{
                //    try
                //    {
                //        _.SetValueAtPropertyPath($"{path}Name", jobj as string);
                //    }
                //    catch { }
                //});
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Type).IsAssignableFrom(objectType);
        }
    }
}