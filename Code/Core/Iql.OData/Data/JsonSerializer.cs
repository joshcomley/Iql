using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypeSharp.Extensions;

namespace Iql.OData.Data
{
    [DoNotConvert]
    public static class JsonSerializer
    {
        private class ShouldSerializeContractResolver : DefaultContractResolver
        {
            private IEnumerable<string> Properties { get; }

            public ShouldSerializeContractResolver(IEnumerable<string> properties)
            {
                Properties = properties;
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                if (Properties.All(propertyName => property.PropertyName != propertyName))
                {
                    property.Ignored = true;
                }
                return property;
            }
        }

        public static string Serialize(object entity, params string[] properties)
        {
            var settings = new JsonSerializerSettings();
            if (properties.Any())
            {
                settings.ContractResolver = new ShouldSerializeContractResolver(properties);
            }
            var json = JsonConvert.SerializeObject(entity, settings);
            return json;
        }
    }
}