using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Entities.Enums;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Services;
using Iql.Entities.SpecialTypes;
using Iql.Parsing.Types;

namespace Iql.Entities
{
    public interface IEntityConfigurationBuilder : IEntityConfigurationBuilderMetadata, IConfiguration, IServiceProviderProvider, ITypeResolver, IMethodContainer
    {
        GuidManager GuidManager { get; }
        SpecialTypeDefinition GetSpecialTypeMap(string name);
        bool IsEntityType<T>();
        bool IsEntityTypeByType(Type type);
        void ForEntityTypes(Func<IEntityConfiguration, bool> filter, Action<IEntityConfiguration> action);
        EntityConfiguration<T> EntityType<T>() where T : class;
        IEnumConfiguration EnumType<T>();
        Dictionary<Type, IList> FlattenObjectGraph(object entity, Type entityType);
        Dictionary<Type, IList> FlattenObjectGraphs(Type entityType, IEnumerable entities);
        //Dictionary<Type, IList> FlattenDependencyGraph(object entity, Type entityType);
        //Dictionary<Type, IList> FlattenDependencyGraphs(Type entityType, IEnumerable entities);
        IEntityConfiguration GetEntityByType(Type type);
        IEntityConfiguration GetEntityByTypeName(string typeName);
        bool IsEntityType(Type type);
    }
}