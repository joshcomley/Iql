using System;
using Iql.Server.Serialization.Deserialization.Converters;
using Newtonsoft.Json;

namespace Iql.Server.Serialization.Deserialization
{
    public class EntityConfigurationParser
    {
        public static EntityConfigurationDocument FromJson(string json)
        {
            var document = DeserializeFromJson<EntityConfigurationDocument>(json);
            return document;
        }

        internal static T DeserializeFromJson<T>(string json, SerializerDetails details = null)
            where T : class
        {
            var settings = NewJsonSerializerSettings<T>(details,
                out var interfaceTypeResolver,
                out var lambdaExpressionConverter,
                out var detailsReturned);
            var result = JsonConvert.DeserializeObject<T>(json, settings);
            var finalisers = new Action<EntityConfigurationDocument>[]
            {
                doc => interfaceTypeResolver.Finalise2(result, doc),
                doc => lambdaExpressionConverter.Finalise(result)
            };
            if (details != null)
            {
                details.PreFinalisers.Add(doc => interfaceTypeResolver.Finalise(result, doc));
                details.Finalisers.AddRange(finalisers);
            }
            else
            {
                foreach (var preFinaliser in detailsReturned.PreFinalisers)
                {
                    preFinaliser(result as EntityConfigurationDocument);
                }
                interfaceTypeResolver.Finalise(result, result as EntityConfigurationDocument);
                foreach (var relationship in (result as EntityConfigurationDocument).AllRelationships())
                {
                    relationship.Source.EntityConfiguration.Relationships.Add(relationship);
                    if (relationship.Source.EntityConfiguration != relationship.Target.EntityConfiguration)
                    {
                        relationship.Target.EntityConfiguration.Relationships.Add(relationship);
                    }
                }
                foreach (var config in (result as EntityConfigurationDocument).EntityTypes)
                {
                    var rels = config.AllRelationships();
                    foreach (var rel in rels)
                    {
                        rel.ThisEnd.Property.Relationship = rel;
                    }
                }

                foreach (var finaliser in detailsReturned.Finalisers)
                {
                    finaliser(result as EntityConfigurationDocument);
                }
                foreach (var finaliser in finalisers)
                {
                    finaliser(result as EntityConfigurationDocument);
                }
            }
            return result;
        }

        public static JsonSerializerSettings NewJsonSerializerSettings<T>(
            SerializerDetails details,
            out InterfaceConverter<T> interfaceTypeResolver,
            out LambdaExpressionConverter<T> lambdaExpressionConverter,
            out SerializerDetails detailsReturned)
        where T : class
        {
            var settings = new JsonSerializerSettings();
            interfaceTypeResolver = new InterfaceConverter<T>(settings, details);
            detailsReturned = interfaceTypeResolver.Details;
            settings.Converters.Add(interfaceTypeResolver);
            lambdaExpressionConverter = new LambdaExpressionConverter<T>();
            settings.Converters.Add(lambdaExpressionConverter);
            settings.Converters.Add(new MetadataCollectionConverter());
            settings.Converters.Add(new IqlExpressionConverter());
            settings.Converters.Add(new TypeConverter(details));
            return settings;
        }
    }
}