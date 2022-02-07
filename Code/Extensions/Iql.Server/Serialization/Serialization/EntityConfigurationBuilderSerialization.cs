using System;
using Iql.Entities;
using Iql.Serialization;
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
            settings.Converters.Add(new RelationshipConverter(entityConfigurationBuilder, false));
            settings.Converters.Add(new ExpressionJsonConverter(entityConfigurationBuilder));
            settings.Converters.Add(new IPropertyConverter(entityConfigurationBuilder));
            settings.Converters.Add(new TypeToStringConverter());
            settings.Formatting = Formatting.Indented;
            try
            {
                var serialized = JsonConvert.SerializeObject(
                    entityConfigurationBuilder,
                    settings);

                return serialized;
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}