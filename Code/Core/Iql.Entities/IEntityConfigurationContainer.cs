using System.Collections.Generic;
using Iql.Entities.Enums;
using Iql.Entities.Functions;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public interface IEntityConfigurationContainer : IUserPermissionContainer
    {
        IEnumerable<IqlMethod> AllMethods();
        IEnumerable<IEntityConfiguration> AllEntityTypes();
        IEnumerable<IEnumConfiguration> AllEnumTypes();
        IEnumerable<IRelationship> AllRelationships();
    }
}