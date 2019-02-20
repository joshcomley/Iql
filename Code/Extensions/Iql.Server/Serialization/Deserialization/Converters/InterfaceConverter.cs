using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Enums;
using Iql.Entities.Geography;
using Iql.Entities.InferredValues;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.SpecialTypes;
using Iql.Server.Serialization.Deserialization.EntityConfiguration;
using Iql.Server.Serialization.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IPropertyGroup = Iql.Entities.IPropertyGroup;

namespace Iql.Server.Serialization.Deserialization.Converters
{
    public class SerializerDetails
    {
        public Dictionary<string, IEntityConfiguration> EntityConfigurations { get; } = new Dictionary<string, IEntityConfiguration>();
        public List<Action<EntityConfigurationDocument>> PreFinalisers { get; } = new List<Action<EntityConfigurationDocument>>();
        public List<Action<EntityConfigurationDocument>> Finalisers { get; } = new List<Action<EntityConfigurationDocument>>();
        public List<Action> OtherFinalisaers { get; } = new List<Action>();
        public int CurrentEntityConfigurationIndex { get; set; }
        public EntityConfigurationBuilder Builder { get; } = new EntityConfigurationBuilder();
    }

    public class InterfaceConverter<T> : JsonConverter
    {
        public Dictionary<string, SerializedPropertyGroup> PropertyMappings { get; } = new Dictionary<string, SerializedPropertyGroup>();
        public JsonSerializerSettings Settings { get; }
        public SerializerDetails Details { get; }
        private Dictionary<Type, Type> TypeMappings { get; } = new Dictionary<Type, Type>();

        public InterfaceConverter(JsonSerializerSettings settings, SerializerDetails details)
        {
            Settings = settings;
            Details = details ?? new SerializerDetails();
            Map<IEnumConfiguration, EnumConfiguration>();
            Map<IEnumValue, EnumValue>();
            Map<IEntityConfiguration, EntityConfiguration.EntityConfiguration>();
            Map<IEntityMetadata, EntityConfiguration.EntityConfiguration>();
            Map<IProperty, Property>();
            Map<IInferredValueConfiguration, InferredValueConfiguration>();
            Map<ITypeDefinition, TypeDetail>();
            Map<IRelationship, Relationship>();
            Map<IRelationshipDetail, RelationshipDetail>();
            Map<IRelationshipRule, RelationshipRule>();
            Map<IRelationshipConstraint, RelationshipConstraint>();
            Map<IRuleCollection<IBinaryRule>, ValidationRuleCollection>();
            Map<IRuleCollection<IDisplayRule>, DisplayRuleCollection>();
            Map<IRuleCollection<IRelationshipRule>, RelationshipRuleCollection>();
            Map<IBinaryRule, ValidationRule>();
            Map<IEntityKey, EntityKeyBase>();
            Map<IDisplayFormatting, DisplayFormatting>();
            Map<IEntityDisplayTextFormatter, DisplayFormatter>();
            Map<IDisplayRule, DisplayRule>();
            Map<IMediaKey, MediaKey>();
            Map<IMediaKeyGroup, MediaKeyGroup>();
            Map<IMediaKeyPart, MediaKeyPart>();
            Map<IGeographicPoint, GeographicPoint>();
            Map<IDateRange, DateRange>();
            Map<SpecialTypeDefinition, SpecialTypeDefinition>();
            Map<IFile, File>();
            Map<IFilePreview, FilePreview>();
            Map<INestedSet, NestedSet>();
            //Map<IPropertyPath, PropertyPath>();
            Map<IPropertyGroup, JObject>();
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


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var path = reader.Path;
            var value = reader.Value;
            var existingValue2 = existingValue;
            if (path.Contains("RelationshipMappings") && 
                (value != null || existingValue2 != null))
            {
                int a = 0;
            }
            //if (reader.TokenType != JsonToken.StartObject)
            //{
            //    var result2 = reader.Read();
            //    return result2;
            //}
            var isConvertedProperty = (objectType == typeof(IRelationshipDetail) || objectType == typeof(IProperty) || objectType == typeof(IPropertyGroup)) && reader.Value is string &&
                                      !string.IsNullOrWhiteSpace(reader.Value as string) &&
                                      (reader.Value as string).StartsWith("{");
            if (isConvertedProperty)
            {
                var group = JsonConvert.DeserializeObject<SerializedPropertyGroup>(reader.Value as string);
                PropertyMappings.Add(reader.Path, group);
                return null;
            }

            if (typeof(IEntityConfiguration).IsAssignableFrom(objectType))
            {
                var match = Regex.Match(path, $@"^{nameof(EntityConfigurationDocument.EntityTypes)}\[(?<Index>\d+)\]");
                if (match.Success)
                {
                    Details.CurrentEntityConfigurationIndex = Convert.ToInt32(match.Groups["Index"].Value);
                }
            }
            var typeMapping = TypeMappings.ContainsKey(objectType) ? TypeMappings[objectType] : objectType;
            if (reader.Path == nameof(EntityConfigurationDocument.UsersDefinition))
            {
                typeMapping = typeof(UsersDefinition);
            }
            else if (reader.Path == nameof(EntityConfigurationDocument.CustomReportsDefinition))
            {
                typeMapping = typeof(CustomReportsDefinition);
            }
            else if (reader.Path == nameof(EntityConfigurationDocument.UserSettingsDefinition))
            {
                typeMapping = typeof(UserSettingsDefinition);
            }

            //if (typeMapping == typeof(File) && existingValue != null && (existingValue as IFile).Previews != null &&
            //    (existingValue as IFile).Previews.Any())
            //{
            //    int a = 0;
            //}
            object result = null;
            if (objectType == typeof(IPropertyGroup) || objectType == typeof(IRelationshipDetail))
            {
                if (objectType == typeof(IRelationshipDetail))
                {
                    int a = 0;
                }
                var jobj = serializer.Deserialize(reader) as JObject;
                if (jobj != null)
                {
                    if (jobj.ContainsKey("Path"))
                    {
                        var propertyPath = EntityConfigurationParser.DeserializeFromJson<PropertyPathJson>(jobj.ToString(), Details);
                        var entityConfigIndex = Details.CurrentEntityConfigurationIndex;
                        Details.Finalisers.Add(doc => propertyPath.SetEntityConfiguration(doc.EntityTypes[entityConfigIndex]));
                        result = propertyPath;
                        //serializer.Deserialize(new JTokenReader(jobj), typeof(PropertyPath));
                    }
                    else
                    {
                        if (objectType == typeof(IPropertyGroup))
                        {
                            var propertyCollection = EntityConfigurationParser.DeserializeFromJson<PropertyCollection>(jobj.ToString(), Details);
                            result = propertyCollection;
                        }
                        else
                        {
                            var relationshipDetail = EntityConfigurationParser.DeserializeFromJson<RelationshipDetail>(jobj.ToString(), Details);
                            result = relationshipDetail;
                        }
                        //result = serializer.Deserialize(new JTokenReader(jobj), typeof(PropertyCollection));
                        //result = jobj.ToObject<PropertyCollection>(serializer);
                    }
                }
            }
            else
            {
                result = serializer.Deserialize(reader, typeMapping);
            }
            if (typeof(IEntityMetadata).IsAssignableFrom(objectType))
            {
                var config = result as IEntityConfiguration;
                Details.EntityConfigurations.Add(config.Name, config);
                Details.PreFinalisers.Insert(0, document => (config as EntityConfiguration.EntityConfiguration).SetConfigurationProvider(document));
            }
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return TypeMappings.ContainsKey(objectType);
        }

        public void Finalise(T root, EntityConfigurationDocument document)
        {
            foreach (var mapping in PropertyMappings)
            {
                if (mapping.Value.Kind != PropertyGroupKind.Relationship)
                {
                    ProcessPropertyGroup(root, document, mapping.Value, mapping, true);
                }
            }
        }

        public void Finalise2(T root, EntityConfigurationDocument document)
        {
            foreach (var mapping in PropertyMappings)
            {
                if (mapping.Value.Kind == PropertyGroupKind.Relationship)
                {
                    ProcessPropertyGroup(root, document, mapping.Value, mapping, true);
                }
            }
        }

        private static IPropertyGroup ProcessPropertyGroup(T root, EntityConfigurationDocument document, SerializedPropertyGroup @group,
            KeyValuePair<string, SerializedPropertyGroup> mapping, bool set)
        {
            var entityMetadata = document.EntityTypes.Single(e => e.Name == @group.Type);
            switch (@group.Kind)
            {
                case PropertyGroupKind.Property:
                    var property = entityMetadata.Properties.Single(p => p.Name == @group.Paths);
                    (property as Property).EntityConfigurationInternal = entityMetadata as IEntityConfiguration;
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, property); }
                    return property;
                case PropertyGroupKind.PropertyCollection:
                    var coll = new PropertyCollection(entityMetadata as IEntityConfiguration);
                    foreach (var child in @group.Children)
                    {
                        coll.Properties.Add(ProcessPropertyGroup(root, document, child, mapping, false));
                    }
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, coll); }
                    return coll;
                case PropertyGroupKind.Geographic:
                    var geo = entityMetadata.Geographics[Convert.ToInt32(@group.Paths)];
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, geo); }
                    return geo;
                case PropertyGroupKind.NestedSet:
                    var ns = entityMetadata.NestedSets[Convert.ToInt32(@group.Paths)];
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, ns); }
                    return ns;
                case PropertyGroupKind.File:
                    var f = entityMetadata.Files[Convert.ToInt32(@group.Paths)];
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, f); }
                    return f;
                case PropertyGroupKind.DateRange:
                    var dr = entityMetadata.DateRanges[Convert.ToInt32(@group.Paths)];
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, dr); }
                    return dr;
                case PropertyGroupKind.Relationship:
                    var entityConfiguration = entityMetadata as IEntityConfiguration;
                    //var property2 = entityMetadata.Properties.Single(p => p.Name == @group.Paths);
                    //var rel = entityMetadata.Relationships.Single(p => p.Source.Property.Name == @group.Paths);
                    //if (set) { document.SetValueAtPropertyPath(mapping.Key, rel.Source); }
                    //return rel.Source;
                    var entityRelationships = entityConfiguration.AllRelationships();
                    var rel = entityRelationships.Single(p => p.ThisEnd.Property.Name == @group.Paths);
                    if (set) { root.SetValueAtPropertyPath(mapping.Key, rel.ThisEnd); }
                    return rel.ThisEnd;
            }

            return null;
        }
    }
}