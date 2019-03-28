using System;
using System.Linq.Expressions;
using Iql.Data.Context;
using Iql.DotNet.Serialization;
using Iql.Parsing.Types;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class ExpressionJsonConverter : JsonConverter
    {
        public ITypeResolver TypeResolver { get; }

        public ExpressionJsonConverter(ITypeResolver typeResolver)
        {
            TypeResolver = typeResolver;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(IqlXmlSerializer.SerializeToXml((LambdaExpression)value, TypeResolver));
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