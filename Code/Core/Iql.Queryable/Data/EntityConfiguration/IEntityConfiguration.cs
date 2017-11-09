using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityConfiguration
    {
        List<IRelationship> Relationships { get; }
        List<IProperty> Properties { get; }
        IEntityKey Key { get; }
        Type Type { get; }
        IProperty GetProperty(string name);
    }
}