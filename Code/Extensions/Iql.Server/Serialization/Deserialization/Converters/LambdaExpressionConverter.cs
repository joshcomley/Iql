using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.DotNet.Serialization;
using Iql.Server.Serialization.Serialization;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Deserialization.Converters
{
    public class LambdaExpressionConverter<T> : JsonConverter
    where T : class
    {
        private Dictionary<string, string> ExpressionMappings { get; } = new Dictionary<string, string>();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ExpressionMappings.Add(reader.Path, reader.Value as string);
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(LambdaExpression).IsAssignableFrom(objectType);
        }

        public void Finalise(T document)
        {
            foreach (var mapping in ExpressionMappings)
            {
                var xml = mapping.Value;
                IqlExpression iql = null;
                if (!string.IsNullOrWhiteSpace(xml))
                {
                    iql = IqlXmlSerializer.DeserializeFromXml(xml);
                }
                document.SetValueAtPropertyPath(mapping.Key + "Iql", iql);
            }
        }
    }
}