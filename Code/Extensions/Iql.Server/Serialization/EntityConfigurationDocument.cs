using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Enums;
using Iql.Entities.Relationships;
using Iql.Entities.SpecialTypes;
using Iql.Server.Serialization.Deserialization;

namespace Iql.Server.Serialization
{
    public class EntityConfigurationDocument : IEntityConfigurationContainer
    {
        public SpecialTypeDefinition UsersDefinition { get; set; }
        public SpecialTypeDefinition CustomReportsDefinition { get; set; }
        public SpecialTypeDefinition UserSettingsDefinition { get; set; }
        public List<IEnumConfiguration> EnumTypes { get; set; } = new List<IEnumConfiguration>();
        public List<IEntityConfiguration> EntityTypes { get; set; } = new List<IEntityConfiguration>();
        public List<IRelationship> Relationships { get; set; } = new List<IRelationship>();

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

        public IEnumerable<IRelationship> AllRelationships()
        {
            return Relationships;
        }

        public UserPermissionsManager Permissions { get; set; }
        public List<IqlUserPermissionRule> PermissionRules { get; set; }
    }
}