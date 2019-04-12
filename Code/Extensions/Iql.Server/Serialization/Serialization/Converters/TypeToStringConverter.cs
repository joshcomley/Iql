using System;
using Iql.Data.Types;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class TypeToStringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                var type = value as Type;
                var iqlType = type.ToIqlType();
                string fullName = iqlType.ToString();
                switch (iqlType)
                {
                    case IqlType.Unknown:
                    case IqlType.Collection:
                        fullName = type.GetFullName();
                        var parsed = TypeName.Parse(fullName);
                        fullName = parsed.FullName;
                        break;
                }

                writer.WriteValue(fullName);
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