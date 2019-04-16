using System.Collections.Generic;
using Iql.Entities;
using Iql.Entities.Enums;
using Iql.Entities.Functions;
using Iql.Entities.Relationships;
using Iql.Entities.SpecialTypes;
using Iql.Server.Serialization.Deserialization;

namespace Iql.Server.Serialization
{
    public class EntityConfigurationDocument : MetadataBase, IEntityConfigurationBuilderMetadata
    {
        public SpecialTypeDefinition UsersDefinition { get; set; }
        public SpecialTypeDefinition CustomReportsDefinition { get; set; }
        public bool ValidateInferredWithClientSide { get; set; }
        public SpecialTypeDefinition UserSettingsDefinition { get; set; }
        public List<IqlMethod> Methods { get; set; } = new List<IqlMethod>();
        public IEnumerable<IEnumConfiguration> EnumTypes { get; set; } = new List<IEnumConfiguration>();
        public IEnumerable<IEntityConfiguration> EntityTypes { get; set; } = new List<IEntityConfiguration>();
        public IEnumerable<IRelationship> Relationships { get; set; } = new List<IRelationship>();

        public static EntityConfigurationDocument FromJson(string json)
        {
            return EntityConfigurationParser.FromJson(json);
        }
        
        public IEnumerable<IqlMethod> AllMethods()
        {
            return Methods;
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

        public UserPermissionsManager PermissionManager { get; }
        public List<IqlUserPermissionRule> PermissionRules { get; set; }
        public override IUserPermission ParentPermissions => null;
    }
}