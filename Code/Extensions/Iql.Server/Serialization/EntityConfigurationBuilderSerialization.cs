using Iql.DotNet.Serialization;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public enum PropertyGroupKind
    {
        Property = 1,
        PropertyCollection,
        Geographic,
        NestedSet
    }

    public class SerializedPropertyGroup
    {
        public string Type { get; set; }
        public string Paths { get; set; }
        public PropertyGroupKind Kind { get; set; }
        public List<SerializedPropertyGroup> Children { get; set; }
    }

    public static class EntityConfigurationBuilderSerialization
    {
        public static string ToJson(this IEntityConfigurationBuilder entityConfigurationBuilder)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new InterfaceContractResolver()
            };
            settings.Converters.Add(new ExpressionJsonConverter());
            settings.Converters.Add(new IPropertyConverter());
            settings.Converters.Add(new TypeConverter());
            var doc = new EntityConfigurationDocument();
            doc.EntityTypes.AddRange(entityConfigurationBuilder.EntityTypes());
            doc.EnumTypes.AddRange(entityConfigurationBuilder.EnumTypes());
            settings.Formatting = Formatting.Indented;
            var serialized = JsonConvert.SerializeObject(doc, settings);
            return serialized;
        }

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

        public class IPropertyConverter : JsonConverter
        {
            public bool IsNested { get; }

            public IPropertyConverter(bool isNested = false)
            {
                IsNested = isNested;
            }
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
                    //if (IsInPropertyOrder)
                    //{
                    //    if (value is PropertyCollection)
                    //    {
                    //        serializer.Serialize(writer, value);
                    //        //WritePropertyGroupDirect(writer, value, false);
                    //    }
                    //    else
                    //    {
                    //        WritePropertyGroupReference(writer, value);
                    //    }
                    //}
                    //else
                    //{

                    //}
                    if (IsNested)
                    {
                        if (string.IsNullOrWhiteSpace(writer.Path))
                        {
                            WritePropertyGroupDirect(writer, value, false, false);
                        }
                        else
                        {
                            WritePropertyGroupReference(writer, value);
                        }
                    }
                    else
                    {
                        var directConversion = string.Join("|", new[]
                        {
                            nameof(IEntityMetadata.Properties),
                            nameof(IEntityMetadata.NestedSets),
                            nameof(IEntityMetadata.Geographics),
                        });
                        if (JsonPathHelper.IsEntityConfigurationProperty(writer.Path, false, directConversion))
                        {
                            // All properties below here must be references
                            WritePropertyGroupDirect(writer, value, false);
                        }
                        //else if (JsonPathHelper.IsEntityConfigurationProperty(writer.Path, true,
                        //             nameof(IEntityMetadata.PropertyOrder)) &&
                        //         value is PropertyCollection)
                        //{
                        //    WritePropertyGroupDirect(writer, value, true);
                        //}
                        else //if (value is IPropertyGroup)
                        {
                            WritePropertyGroupReference(writer, value);
                        }
                    }
                }
            }

            private static void WritePropertyGroupReference(JsonWriter writer, object value)
            {
                var serialized = SerializePropertyGroup(value as IPropertyGroup);
                var json = JsonConvert.SerializeObject(serialized);
                writer.WriteValue(json);
            }

            private static void WritePropertyGroupDirect(JsonWriter writer, object value, bool allowDirectPropertyConversion = true, bool allowAnyPropertyConversion = true)
            {
                var indented = Formatting.Indented;
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new InterfaceContractResolver()
                };
                settings.Converters.Add(new ExpressionJsonConverter());
                settings.Converters.Add(new TypeConverter());
                if (allowAnyPropertyConversion)
                {
                    settings.Converters.Add(new IPropertyConverter(true));
                }
                //settings.Converters.Add(new IPropertyConverter());
                var ppp = writer.Path;
                var serialized = JsonConvert.SerializeObject(value, value.GetType(), indented, settings);
                writer.WriteRawValue(serialized);
            }

            private static SerializedPropertyGroup SerializePropertyGroup(IPropertyGroup propertyGroup)
            {
                if (propertyGroup != null)
                {
                    var properties = propertyGroup.GetProperties();
                    var entityConfiguration = propertyGroup.EntityConfiguration;
                    var kind = PropertyGroupKind.Property;
                    List<SerializedPropertyGroup> children = null;
                    string path = null;
                    if (propertyGroup is IGeographic)
                    {
                        kind = PropertyGroupKind.Geographic;
                        path = entityConfiguration.Geographics.IndexOf(propertyGroup as IGeographic).ToString();
                    }
                    else if (propertyGroup is INestedSet)
                    {
                        kind = PropertyGroupKind.NestedSet;
                        path = entityConfiguration.NestedSets.IndexOf(propertyGroup as INestedSet).ToString();
                    }
                    else if (propertyGroup is PropertyCollection)
                    {
                        throw new NotImplementedException();
                    }

                    switch (kind)
                    {
                        case PropertyGroupKind.Property:
                            path = (properties[0] as IProperty).Name;
                            break;
                        case PropertyGroupKind.PropertyCollection:
                            children = new List<SerializedPropertyGroup>();
                            foreach (var child in properties)
                            {
                                children.Add(SerializePropertyGroup(child)); ;
                            }
                            // path = string.Join(",", properties.Select(p => p.Name));
                            break;
                    }
                    var serialized = new SerializedPropertyGroup
                    {
                        Kind = kind,
                        Type = entityConfiguration.Type.Name,
                        Paths = path,
                        Children = children
                    };
                    return serialized;
                }

                return null;
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
                if (IsNested)
                {
                    return typeof(IProperty).IsAssignableFrom(objectType);
                }
                return !typeof(PropertyCollection).IsAssignableFrom(objectType) && typeof(IPropertyGroup).IsAssignableFrom(objectType);
            }
        }

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

        private class InterfaceContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                if (typeof(IEntityConfiguration).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IEntityMetadata), memberSerialization);
                }
                if (typeof(IProperty).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IPropertyMetadata), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IPropertyMetadata.HasMediaKey) && p.PropertyName != nameof(IProperty.EntityConfiguration))
                        .ToList();
                }
                if (typeof(ITypeDefinition).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(ITypeConfiguration), memberSerialization);
                }
                if (typeof(IRelationship).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IRelationship), memberSerialization);
                }
                if (typeof(IRelationshipDetail).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IRelationshipDetailMetadata), memberSerialization);
                }
                if (typeof(IDisplayFormatting).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IDisplayFormatting), memberSerialization);
                }
                if (typeof(IEntityDisplayTextFormatter).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IEntityDisplayTextFormatter), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IEntityDisplayTextFormatter.Format))
                        .ToList();
                }
                if (typeof(IDisplayRule).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IDisplayRule), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                        .ToList();
                }
                if (typeof(IGeographic).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IGeographic), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IGeographic.EntityConfiguration))
                        .ToList();
                }
                if (typeof(INestedSet).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(INestedSet), memberSerialization)
                        .Where(p => p.PropertyName != nameof(INestedSet.EntityConfiguration))
                        .ToList();
                }
                if (typeof(IMediaKey).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IMediaKey), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IMediaKey.Property))
                        .ToList();
                }
                if (typeof(IMediaKeyGroup).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IMediaKeyGroup), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IMediaKeyGroup.MediaKey))
                        .ToList();
                }
                if (typeof(IMediaKeyPart).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IMediaKeyPart), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IMediaKeyPart.MediaKey))
                        .ToList();
                }
                if (typeof(IEntityKey).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IEntityKey), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IEntityKey.Type) &&
                                    p.PropertyName != nameof(IEntityKey.KeyType))
                        .ToList();
                }
                if (typeof(IRelationshipRule).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IRelationshipRule), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                        .ToList();
                }
                if (typeof(IBinaryRule).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IBinaryRule), memberSerialization)
                        .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                        .ToList();
                }

                if (typeof(IPropertyGroup).IsAssignableFrom(type))
                {
                    return base.CreateProperties(type, memberSerialization)
                        .Where(p => p.PropertyName != nameof(IProperty.EntityConfiguration))
                        .ToList();
                }
                //if (type == typeof(IRuleCollection<IBinaryRule>))
                //{
                //    return base.CreateProperties(typeof(IRuleCollection<IBinaryRule>), memberSerialization);
                //}
                //if (typeof(IRuleCollection<IDisplayRule>).IsAssignableFrom(type))
                //{
                //    int a = 0;
                //}
                //if (typeof(IRuleCollection<IRelationshipRule>).IsAssignableFrom(type))
                //{
                //    int a = 0;
                //}

                //if (type == typeof(IBinaryRule))
                //{
                //    int a = 0;
                //}
                //IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
                return base.CreateProperties(type, memberSerialization);
            }
        }
    }
}