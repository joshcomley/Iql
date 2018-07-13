using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Enums;

namespace Iql.Server.Serialization
{
    public class EntityConfigurationDocument
    {
        public List<IEnumConfiguration> EnumTypes { get; set; } = new List<IEnumConfiguration>();
        public List<IEntityMetadata> EntityTypes { get; set; } = new List<IEntityMetadata>();

        public static EntityConfigurationDocument FromJson(string json)
        {
            return EntityConfigurationParser.FromJson(json);
        }
    }
}