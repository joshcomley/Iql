using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityConfigurationBuilder
    {
        IEnumerable<IEntityConfiguration> AllConfigurations();
        EntityConfiguration<T> EntityType<T>() where T : class;
        Dictionary<Type, IList> FlattenObjectGraph(object entity, Type entityType);
        Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities);
        EntityConfiguration<T> GetEntity<T>() where T : class;
        IEntityConfiguration GetEntityByType(Type type);
        bool IsEntityType(Type type);
    }
}