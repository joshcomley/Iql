using System.Collections.Generic;
using Iql.Entities.Enums;
using Iql.Entities.Functions;
using Iql.Entities.Relationships;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public interface IEntityConfigurationBuilderMetadata : IEntityConfigurationContainer, IConfiguration
    {
        List<IqlMethod> Methods { get; set; }
        IEnumerable<IEnumConfiguration> EnumTypes { get; }
        IEnumerable<IEntityConfiguration> EntityTypes { get; }
        IEnumerable<IRelationship> Relationships { get; }
        SpecialTypeDefinition UsersDefinition { get; set; }
        SpecialTypeDefinition UserSettingsDefinition { get; set; }
        SpecialTypeDefinition CustomReportsDefinition { get; set; }
        bool ValidateInferredWithClientSide { get; set; }
    }
}