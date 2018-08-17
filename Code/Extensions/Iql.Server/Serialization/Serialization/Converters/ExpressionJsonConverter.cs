using System;
using System.Linq.Expressions;
using Iql.DotNet.Serialization;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class ExpressionJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(IqlXmlSerializer.SerializeToXml((LambdaExpression)value));
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
            return typeof(Expression).IsAssignableFrom(objectType);
        }
    }
}