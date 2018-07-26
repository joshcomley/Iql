using Iql.DotNet.Serialization;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Enums;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public class Relationship : RelationshipBase
    {

    }

    [DebuggerDisplay("{Name} - {SetName}")]
    public class EntityConfiguration : EntityConfigurationBase
    {

    }

    public class ValidationRuleCollection : RuleCollection<IBinaryRule>
    {

    }

    public class DisplayRuleCollection : RuleCollection<IDisplayRule>
    {

    }
    public class RelationshipRuleCollection : RuleCollection<IRelationshipRule>
    {

    }
    public abstract class RuleBase : IRule
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public LambdaExpression Expression { get; set; }
        public IqlExpression ExpressionIql { get; set; }
    }

    public class DisplayFormatter : IEntityDisplayTextFormatter
    {
        public string Key { get; }
        public LambdaExpression FormatterExpression { get; set; }
        public IqlExpression FormatterExpressionIql { get; set; }
        public Func<object, string> Format { get; }
    }

    public class DisplayFormatting : IDisplayFormatting
    {
        public DisplayFormatting()
        {
            All = new List<IEntityDisplayTextFormatter>();
        }
        public IEnumerable<IEntityDisplayTextFormatter> All { get; set; }
        public IEntityDisplayTextFormatter Default { get; set; }
        public IEntityDisplayTextFormatter Get(string key)
        {
            throw new NotImplementedException();
        }

        public IEntityDisplayTextFormatter Set(Expression<Func<object, string>> expression, string key = null)
        {
            throw new NotImplementedException();
        }
    }

    public class RelationshipRule : RuleBase, IRelationshipRule
    {
        public Func<object, LambdaExpression> Run { get; }
    }

    public class DisplayRule : RuleBase, IDisplayRule
    {
        public Func<object, bool> Run { get; }
        public DisplayRuleKind Kind { get; set; }
        public DisplayRuleAppliesToKind AppliesToKind { get; set; }
    }

    public class ValidationRule : RuleBase, IBinaryRule, IValidationRule
    {
        public Func<object, bool> Run { get; }
    }

    [DebuggerDisplay("{Name}")]
    public class Property : PropertyBase, IProperty
    {
        private IMediaKey _mediaKey;
        protected override IMediaKey GetMediaKey()
        {
            return _mediaKey;
        }

        protected override void SetMediaKey(IMediaKey value)
        {
            _mediaKey = value;
        }

        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }
        public Dictionary<string, object> CustomInformation { get; }
    }

    public class MediaKey : MediaKeyBase
    {
        protected override void ClearGroups()
        {
            throw new NotImplementedException();
        }
    }

    public class MediaKeyGroup : IMediaKeyGroup
    {
        public IMediaKey MediaKey { get; }
        public string Separator { get; set; }
        public List<IMediaKeyPart> Parts { get; set; }
        public string[] Evaluate(object entity)
        {
            throw new NotImplementedException();
        }

        public string EvaluateToString(object entity)
        {
            throw new NotImplementedException();
        }
    }

    public class EntityConfigurationParser
    {
        public class TypeConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return null;
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(Type).IsAssignableFrom(objectType);
            }
        }

        public class LambdaExpressionConverter : JsonConverter
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

            public void Finalise(EntityConfigurationDocument document)
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


        public class MetadataCollectionConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var mcj = new MetadataCollectionJson();
                serializer.Populate(reader, mcj);
                var mc = new MetadataCollection();
                foreach (var item in mcj.All)
                {
                    mc.Set(item.Key, item.Value);
                }
                return mc;
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(IMetadataCollection).IsAssignableFrom(objectType);
            }
        }

        internal class MetadataCollectionJson : MetadataCollectionBase, IMetadataCollection
        {
            public KeyValuePair<string, object>[] All { get; set; }
        }

        public class InterfaceConverter : JsonConverter
        {
            private Dictionary<Type, Type> TypeMappings { get; } = new Dictionary<Type, Type>();

            public InterfaceConverter()
            {
                Map<IEnumConfiguration, EnumConfiguration>();
                Map<IEnumValue, EnumValue>();
                Map<IEntityMetadata, EntityConfiguration>();
                Map<IProperty, Property>();
                Map<ITypeDefinition, TypeDetail>();
                Map<IRelationship, Relationship>();
                Map<IRelationshipConstraint, RelationshipConstraint>();
                Map<IRuleCollection<IBinaryRule>, ValidationRuleCollection>();
                Map<IRuleCollection<IDisplayRule>, DisplayRuleCollection>();
                Map<IRuleCollection<IRelationshipRule>, RelationshipRuleCollection>();
                Map<IBinaryRule, ValidationRule>();
                Map<IEntityKey, EntityKeyBase>();
                Map<IDisplayFormatting, DisplayFormatting>();
                Map<IEntityDisplayTextFormatter, DisplayFormatter>();
                Map<IDisplayRule, DisplayRule>();
                Map<IRelationshipRule, RelationshipRule>();
                Map<IMediaKey, MediaKey>();
                Map<IMediaKeyGroup, MediaKeyGroup>();
                Map<IMediaKeyPart, MediaKeyPart>();
                Map<IGeographic, Geographic>();
                Map<INestedSet, NestedSet>();
                Map<IPropertyGroup, PropertyCollection>();
                // Map<IMetadataCollection, MetadataCollectionJson>();
            }

            private void Map<TInterface, TConcrete>()
            // where TConcrete : TInterface
            {
                TypeMappings.Add(typeof(TInterface), typeof(TConcrete));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            private Dictionary<string, IEntityConfiguration> EntityConfigurations { get; } = new Dictionary<string, IEntityConfiguration>();
            private Dictionary<string, SerializedPropertyGroup> PropertyMappings { get; } = new Dictionary<string, SerializedPropertyGroup>();

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var path = reader.Path;
                var value = reader.Value;
                var existingValue2 = existingValue;
                //if (reader.TokenType != JsonToken.StartObject)
                //{
                //    var result2 = reader.Read();
                //    return result2;
                //}
                var isConvertedProperty = (objectType == typeof(IProperty) || objectType == typeof(IPropertyGroup)) && reader.Value is string &&
                                          !string.IsNullOrWhiteSpace(reader.Value as string) &&
                                          (reader.Value as string).StartsWith("{");
                if (isConvertedProperty)
                {
                    if (!string.IsNullOrWhiteSpace(reader.Value as string))
                    {
                        var group = JsonConvert.DeserializeObject<SerializedPropertyGroup>(reader.Value as string);
                        PropertyMappings.Add(reader.Path, group);
                    }
                    return null;
                }

                var result = serializer.Deserialize(reader, TypeMappings[objectType]);
                if (objectType == typeof(IEntityConfiguration))
                {
                    var config = result as IEntityConfiguration;
                    EntityConfigurations.Add(config.Name, config);
                }
                return result;
            }

            public override bool CanConvert(Type objectType)
            {
                return TypeMappings.ContainsKey(objectType);
            }

            public void Finalise(EntityConfigurationDocument document)
            {
                foreach (var mapping in PropertyMappings)
                {
                    ProcessPropertyGroup(document, mapping.Value, mapping, true);
                }
            }

            private static IPropertyGroup ProcessPropertyGroup(EntityConfigurationDocument document, SerializedPropertyGroup @group,
                KeyValuePair<string, SerializedPropertyGroup> mapping, bool set)
            {
                var entityMetadata = document.EntityTypes.Single(e => e.Name == @group.Type);
                switch (@group.Kind)
                {
                    case PropertyGroupKind.Property:
                        var property = entityMetadata.Properties.Single(p => p.Name == @group.Paths);
                        if (set) { document.SetValueAtPropertyPath(mapping.Key, property); }
                        return property;
                    case PropertyGroupKind.PropertyCollection:
                        var coll = new PropertyCollection(entityMetadata as IEntityConfiguration);
                        foreach (var child in @group.Children)
                        {
                            coll.Properties.Add(ProcessPropertyGroup(document, child, mapping, false));
                        }
                        if (set) { document.SetValueAtPropertyPath(mapping.Key, coll); }
                        return coll;
                    case PropertyGroupKind.Geographic:
                        var geo = entityMetadata.Geographics[Convert.ToInt32(@group.Paths)];
                        if (set) { document.SetValueAtPropertyPath(mapping.Key, geo); }
                        return geo;
                    case PropertyGroupKind.NestedSet:
                        var ns = entityMetadata.NestedSets[Convert.ToInt32(@group.Paths)];
                        if (set) { document.SetValueAtPropertyPath(mapping.Key, ns); }
                        return ns;
                }

                return null;
            }
        }

        public static EntityConfigurationDocument FromJson(string json)
        {
            var settings = new JsonSerializerSettings();
            var interfaceTypeResolver = new InterfaceConverter();
            settings.Converters.Add(interfaceTypeResolver);
            var lambdaExpressionConverter = new LambdaExpressionConverter();
            settings.Converters.Add(lambdaExpressionConverter);
            settings.Converters.Add(new MetadataCollectionConverter());
            var configurationDocument = JsonConvert.DeserializeObject<EntityConfigurationDocument>(json, settings);
            settings.Converters.Add(new TypeConverter());
            interfaceTypeResolver.Finalise(configurationDocument);
            lambdaExpressionConverter.Finalise(configurationDocument);
            return configurationDocument;
        }
    }
}