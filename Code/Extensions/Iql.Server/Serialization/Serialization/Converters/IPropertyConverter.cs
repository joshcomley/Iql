using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Parsing.Types;
using Iql.Server.Serialization.Serialization.Resolvers;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Serialization.Converters
{
    public class IPropertyConverter : JsonConverter
    {
        public ITypeResolver TypeResolver { get; }
        public bool IsNested { get; }
        public bool SkipFirst { get; }
        public string Path { get; }
        private bool _hasSkippedFirst = false;
        public IPropertyConverter(ITypeResolver typeResolver, bool isNested = false, bool skipFirst = false, string path = "")
        {
            TypeResolver = typeResolver;
            IsNested = isNested;
            SkipFirst = skipFirst;
            Path = path;
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
                if (IsNested)
                {
                    if (writer.Path == nameof(IRelationship.Source) ||
                        writer.Path == nameof(IRelationship.Target))
                    {
                        WritePropertyGroupDirect(TypeResolver, writer, value, Path,  true, true);
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
                    if (JsonPathHelper.IsEntityConfigurationProperty(path, false, $"({directConversion})"))
                    {
                        // All properties below here must be references
                        WritePropertyGroupDirect(TypeResolver, writer, value, Path);
                    }
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

        public static Dictionary<Tuple<ITypeResolver, bool, bool>, JsonSerializerSettings> _settingsLookup =
            new Dictionary<Tuple<ITypeResolver, bool, bool>, JsonSerializerSettings>();
        private static void WritePropertyGroupDirect(
            ITypeResolver typeResolver,
            JsonWriter writer,
            object value,
            string currentPath,
            bool allowAnyPropertyConversion = true,
            bool rootIsRelationship = false)
        {
            var key = new Tuple<ITypeResolver, bool, bool>(typeResolver, allowAnyPropertyConversion,
                rootIsRelationship);
            JsonSerializerSettings settings;
            if (!_settingsLookup.ContainsKey(key))
            {
                settings = new JsonSerializerSettings
                {
                    ContractResolver = new InterfaceContractResolver()
                };
                if (!rootIsRelationship)
                {
                    settings.Converters.Add(new RelationshipConverter(typeResolver, true));
                }
                settings.Converters.Add(new ExpressionJsonConverter(typeResolver));
                settings.Converters.Add(new TypeToIqlTypeConverter());
                //settings.Converters.Add(new IPropertyConverter());
                _settingsLookup.Add(key, settings);
            }
            else
            {
                settings = _settingsLookup[key];
            }

            var existingPropertyConverter = settings.Converters.SingleOrDefault(_ => _ is IPropertyConverter);
            settings.Converters.Remove(existingPropertyConverter);
            settings.Converters.Add(new IPropertyConverter(typeResolver, true, allowAnyPropertyConversion,
                $"{currentPath}/{writer.Path}".Trim('/')));

            var serialized = JsonConvert.SerializeObject(value, value.GetType(), Formatting.Indented, settings);
            writer.WriteRawValue(serialized);
        }

        private static SerializedPropertyGroup SerializePropertyGroup(IPropertyGroup propertyGroup)
        {
            if (propertyGroup != null)
            {
                var properties = propertyGroup.GetGroupProperties();
                var entityConfiguration = propertyGroup.EntityConfiguration;
                var kind = IqlPropertyGroupKind.Property;
                List<SerializedPropertyGroup> children = null;
                string path = null;
                if (propertyGroup is IGeographicPoint)
                {
                    kind = IqlPropertyGroupKind.Geographic;
                    path = entityConfiguration.Geographics.IndexOf(propertyGroup as IGeographicPoint).ToString();
                }
                else if (propertyGroup is INestedSet)
                {
                    kind = IqlPropertyGroupKind.NestedSet;
                    path = entityConfiguration.NestedSets.IndexOf(propertyGroup as INestedSet).ToString();
                }
                else if (propertyGroup is IDateRange)
                {
                    kind = IqlPropertyGroupKind.DateRange;
                    path = entityConfiguration.DateRanges.IndexOf(propertyGroup as IDateRange).ToString();
                }
                else if (propertyGroup is IFile)
                {
                    kind = IqlPropertyGroupKind.File;
                    path = entityConfiguration.Files.IndexOf(propertyGroup as IFile).ToString();
                }
                else if (propertyGroup is IRelationshipDetailMetadata)
                {
                    kind = IqlPropertyGroupKind.Relationship;
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
                    case IqlPropertyGroupKind.Property:
                        path = (properties[0] as IProperty).Name;
                        break;
                    case IqlPropertyGroupKind.PropertyCollection:
                        children = new List<SerializedPropertyGroup>();
                        foreach (var child in properties)
                        {
                            children.Add(SerializePropertyGroup(child)); ;
                        }
                        // path = string.Join(",", properties.Select(p => p.Name));
                        break;
                }

                if (path == "InferredChainFromSelf")
                {
                    int a = 0;
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
            if (SkipFirst && !_hasSkippedFirst)
            {
                _hasSkippedFirst = true;
                return false;
            }
            if (IsNested)
            {
                return typeof(IProperty).IsAssignableFrom(objectType) || typeof(IRelationshipDetail).IsAssignableFrom(objectType);
            }
            return
                !typeof(IPropertyCollection).IsAssignableFrom(objectType) && !typeof(IPropertyPath).IsAssignableFrom(objectType) &&
                typeof(IPropertyGroup).IsAssignableFrom(objectType);
        }
    }
}