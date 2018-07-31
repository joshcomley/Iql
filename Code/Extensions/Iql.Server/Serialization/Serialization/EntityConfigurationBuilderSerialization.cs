using Iql.Entities;
using Newtonsoft.Json;
using Iql.Server.Serialization.Converters;
using Iql.Server.Serialization.Resolvers;

namespace Iql.Server.Serialization
{
    public static class EntityConfigurationBuilderSerialization
    {
        public static string ToJson(this IEntityConfigurationBuilder entityConfigurationBuilder)
        {
            var settings = new JsonSerializerSettings
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
    }
}