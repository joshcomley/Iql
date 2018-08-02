using System.Collections.Generic;
using Iql.Entities.Enums;

namespace Iql.Entities
{
    public interface IEntityConfigurationContainer
    {
        IEnumerable<IEntityConfiguration> AllEntityTypes();
        IEnumerable<IEnumConfiguration> AllEnumTypes();
    }
}