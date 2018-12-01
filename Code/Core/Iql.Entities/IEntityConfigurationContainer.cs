﻿using System.Collections.Generic;
using Iql.Entities.Enums;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public interface IEntityConfigurationContainer
    {
        IEnumerable<IEntityConfiguration> AllEntityTypes();
        IEnumerable<IEnumConfiguration> AllEnumTypes();
        IEnumerable<IRelationship> AllRelationships();
    }
}