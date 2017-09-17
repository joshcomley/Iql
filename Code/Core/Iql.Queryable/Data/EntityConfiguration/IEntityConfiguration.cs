using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityConfiguration
    {
        List<IRelationship> Relationships { get; }
        List<IKeyProperty> Properties { get; }
        IEntityKey Key { get; }
        Type Type { get; }
        IKeyProperty GetProperty(string name);
    }
}