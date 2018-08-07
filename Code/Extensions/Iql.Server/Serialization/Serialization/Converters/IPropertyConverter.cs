using System;
using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Server.Serialization.Resolvers;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Converters
{
    public class IPropertyConverter : JsonConverter
    {
        public bool IsNested { get; }

        public IPropertyConverter(bool isNested = false)
        {
            IsNested = isNested;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var path = writer.Path;
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
                        nameof(IEntityMetadata.DateRanges),
                        nameof(IEntityMetadata.Files),
                        $@"{nameof(IEntityMetadata.Relationships)}\[[0-9]+\]\.({nameof(IRelationship.Source)}|{nameof(IRelationship.Target)})",
                    });
                    if (JsonPathHelper.IsEntityConfigurationProperty(writer.Path, false, $"({directConversion})"))
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
                var properties = propertyGroup.GetGroupProperties();
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
                else if (propertyGroup is IDateRange)
                {
                    kind = PropertyGroupKind.DateRange;
                    path = entityConfiguration.DateRanges.IndexOf(propertyGroup as IDateRange).ToString();
                }
                else if (propertyGroup is IFile)
                {
                    kind = PropertyGroupKind.File;
                    path = entityConfiguration.Files.IndexOf(propertyGroup as IFile).ToString();
                }
                else if (propertyGroup is IRelationshipDetailMetadata)
                {
                    kind = PropertyGroupKind.Relationship;
                    path = (propertyGroup as IRelationshipDetailMetadata).Property.Name;
                    //var index = 0;
                    //var isSource = false;
                    //for (var i = 0; i < entityConfiguration.Relationships.Count; i++)
                    //{
                    //    if (entityConfiguration.Relationships[i].Source == propertyGroup)
                    //    {
                    //        isSource = true;
                    //        index = i;
                    //        break;
                    //    }

                    //    if (entityConfiguration.Relationships[i].Target == propertyGroup)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //path = $"{index}:{(isSource ? "Source" : "Target")}";
                }
                else if (propertyGroup is IPropertyCollection)
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
            return !typeof(IPropertyCollection).IsAssignableFrom(objectType) && !typeof(IPropertyPath).IsAssignableFrom(objectType) && typeof(IPropertyGroup).IsAssignableFrom(objectType);
        }
    }
}