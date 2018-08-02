using System;
using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Enums;

namespace Iql.Server.Serialization
{
    public class EntityConfigurationDocument : Entities.IEntityConfigurationProvider
    {
        public List<IEnumConfiguration> EnumTypes { get; set; } = new List<IEnumConfiguration>();
        public List<IEntityConfiguration> EntityTypes { get; set; } = new List<IEntityConfiguration>();

        public static EntityConfigurationDocument FromJson(string json)
        {
            return EntityConfigurationParser.FromJson(json);
        }

        public IEnumerable<IEntityConfiguration> AllEntityTypes()
        {
            return EntityTypes;
        }

        public IEnumerable<IEnumConfiguration> AllEnumTypes()
        {
            return EnumTypes;
        }
    }
}