using System;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class TypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if (value != null)
            //{
            //    writer.WriteValue((value as IProperty).Name);
            //}
            //writer.WriteValue((value as IProperty).Name);
            //writer.WriteRaw("{}");
            if (value != null)
            {
                writer.WriteValue((value as Type).ToIqlType().ToString());
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Type).IsAssignableFrom(objectType);
        }
    }
}