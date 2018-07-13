using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Iql.DotNet.Serialization;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Iql.Server.Serialization
{
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
                    if (Regex.IsMatch(writer.Path, @"^EntityTypes\[[0-9]+\]\.Properties(|\[[0-9]+\])$"))
                    {
                        var indented = Formatting.Indented;
                        var settings = new JsonSerializerSettings
                        {
                            ContractResolver = new InterfaceContractResolver()
                        };
                        settings.Converters.Add(new ExpressionJsonConverter());
                        settings.Converters.Add(new TypeConverter());
                        //settings.Converters.Add(new IPropertyConverter());
                        var serialized = JsonConvert.SerializeObject(value, value.GetType(), indented, settings);
                        writer.WriteRawValue(serialized);
                    }
                    else
                    {
                        var property = value as IProperty;
                        writer.WriteValue($"{property.EntityConfiguration.Type.Name}:{property.Name}");
                    }
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
                return typeof(IProperty).IsAssignableFrom(objectType);
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

        class InterfaceContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                if (typeof(IEntityConfiguration).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IEntityMetadata), memberSerialization);
                }
                if (typeof(IProperty).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IPropertyMetadata), memberSerialization);
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