using System;
using Iql.Entities;
using Iql.Server.Serialization.Serialization.Converters;
using Iql.Server.Serialization.Serialization.Resolvers;
using Newtonsoft.Json;
using TypeConverter = Iql.Server.Serialization.Deserialization.Converters.TypeConverter;

namespace Iql.Server.Serialization.Serialization
{
    public static class EntityConfigurationBuilderSerialization
    {
        public static string ToJson(this IEntityConfigurationBuilder entityConfigurationBuilder)
        {

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new InterfaceContractResolver(),
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            settings.Converters.Add(new RelationshipConverter(false));
            settings.Converters.Add(new ExpressionJsonConverter());
            settings.Converters.Add(new IPropertyConverter());
            settings.Converters.Add(new TypeConverter());
            settings.Formatting = Formatting.Indented;
            try
            {
                var serialized = JsonConvert.SerializeObject(
                    ToSerializableDocument(entityConfigurationBuilder),
                    settings);

                return serialized;
            }
            catch (Exception e)
            {
                int a = 0;
            }
            return null;
        }

        private static EntityConfigurationDocument ToSerializableDocument(
            IEntityConfigurationBuilder entityConfigurationBuilder)
        {
            var doc = new EntityConfigurationDocument();
            doc.EntityTypes.AddRange(entityConfigurationBuilder.AllEntityTypes());
            doc.EnumTypes.AddRange(entityConfigurationBuilder.AllEnumTypes());
            doc.Relationships.AddRange(entityConfigurationBuilder.AllRelationships());
            doc.UsersDefinition = entityConfigurationBuilder.UsersDefinition;
            doc.CustomReportsDefinition = entityConfigurationBuilder.CustomReportsDefinition;
            doc.UserSettingsDefinition = entityConfigurationBuilder.UserSettingsDefinition;
            return doc;
        }
    }
}