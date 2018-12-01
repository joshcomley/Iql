using System;
using System.Text.RegularExpressions;
using Iql.Entities.Relationships;
using Iql.Server.Serialization.Serialization.Resolvers;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class RelationshipConverter : JsonConverter
    {
        public bool Nested { get; }
        public bool SkipFirst { get; }
        private bool _hasSkipFirst = false;

        public RelationshipConverter(bool nested, bool skipFirst = false)
        {
            Nested = nested;
            SkipFirst = skipFirst;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!Nested && (writer.Path == nameof(EntityConfigurationDocument.Relationships)||
                Regex.IsMatch(writer.Path, $@"^{nameof(EntityConfigurationDocument.Relationships)}\[\d+\]$")))
            {
                WriteRelationshipDirect(writer, value);
            }
            else
            {
                //writer.WriteValue("Some Relationship Reference");
            }
        }

        private static void WriteRelationshipDirect(JsonWriter writer, object value, bool allowAnyPropertyConversion = true)
        {
            var indented = Formatting.Indented;
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new InterfaceContractResolver()
            };
            settings.Converters.Add(new RelationshipConverter(true, true));
            settings.Converters.Add(new ExpressionJsonConverter());
            settings.Converters.Add(new TypeConverter());
            settings.Converters.Add(new IPropertyConverter(true, allowAnyPropertyConversion));
            //settings.Converters.Add(new IPropertyConverter());
            var ppp = writer.Path;
            var serialized = JsonConvert.SerializeObject(value, value.GetType(), indented, settings);
            writer.WriteRawValue(serialized);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (SkipFirst && !_hasSkipFirst)
            {
                _hasSkipFirst = true;
                return false;
            }
            return typeof(IRelationship).IsAssignableFrom(objectType);
        }
    }
}