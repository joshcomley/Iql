using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Entities.Enums;

namespace Iql.Entities
{
    public interface IEntityConfigurationBuilder : IEntityConfigurationProvider
    {
        bool IsEntityType<T>();
        bool IsEntityTypeByType(Type type);
        void ForEntityTypes(Func<IEntityConfiguration, bool> filter, Action<IEntityConfiguration> action);
        EntityConfiguration<T> EntityType<T>() where T : class;
        IEnumConfiguration EnumType<T>();
        Dictionary<Type, IList> FlattenObjectGraph(object entity, Type entityType);
        Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities);
        Dictionary<Type, IList> FlattenDependencyGraph(object entity, Type entityType);
        Dictionary<Type, IList> FlattenDependencyGraphs(Type entityType, IEnumerable entities);
        IEntityConfiguration GetEntityByType(Type type);
        bool IsEntityType(Type type);
    }
}