using System.Collections.Generic;
using Iql.Entities.Enums;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public interface IEntityConfigurationContainer : IUserPermissionContainer
    {
        IEnumerable<IEntityConfiguration> AllEntityTypes();
        IEnumerable<IEnumConfiguration> AllEnumTypes();
        IEnumerable<IRelationship> AllRelationships();
    }
}