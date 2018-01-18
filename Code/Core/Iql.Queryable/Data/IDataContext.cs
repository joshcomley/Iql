using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
{
    public interface IDataContext
    {
        EvaluateContext EvaluateContext { get; set; }
        IDataStore DataStore { get; }
        EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        void RegisterConfiguration<T>(T configuration)
            where T : class;

        IEntityStateBase GetEntityState(object entity
#if TypeScript
            , Type entityType
#endif
        );
        T GetConfiguration<T>()
            where T : class;

        IDbSet AsDbSetByType(Type entityType);
        DbSet<T, TKey> AsDbSet<T, TKey>() where T : class;
        bool IsIdMatch(object left, object right, Type type);
        bool EntityPropertiesMatch(object entity, CompositeKey key);
        bool EntityHasKey(object left, Type type, CompositeKey key);
        void DeleteEntity(object entity
#if TypeScript
            , Type entityType
#endif
        );
        void CascadeDeleteEntity(object entity, object cascadedFromEntity, IRelationship relationship
#if TypeScript
            , Type entityType, Type cascadedFromEntityType
#endif
        );
        void AddEntity(object entity
#if TypeScript
            , Type entityType
#endif
        );
        Task<T> RefreshEntity<T>(T entity
#if TypeScript
            , Type entityType
#endif
            )
            where T : class
        ;

        Task<T> GetEntityByMockEntity<T>(
            T entity
#if TypeScript
            , Type entityType
#endif
            )
            where T : class
        ;

        T EnsureTypedEntity<T>(object entity) where T : class;
        object EnsureTypedEntityByType(object entity, Type type);
        IList<T> EnsureTypedList<T>(IEnumerable responseData, bool forceNotNull = false) where T : class;
        IList EnsureTypedListByType(IEnumerable responseData, Type type, object owner, Type childType, bool forceNotNull = false);
    }
}