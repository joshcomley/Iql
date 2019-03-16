using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.DataStores.NestedSets;
using Iql.Data.Lists;
using Iql.Data.SpecialTypes;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Entities.Validation.Validation;
using Iql.Events;
using Iql.Parsing;

namespace Iql.Data.Context
{
    public interface IDataContext : IServiceProviderProvider
    {
        EventEmitter<OfflineChangeStateChangedEvent> OfflineStateChanged { get; }
        bool EnableOffline { get; set; }
        bool SupportsOffline { get; }
        IOfflineDataStore OfflineDataStore { get; set; }
        IPersistState PersistState { get; set; }
        bool RefreshDisabled { get; set; }
        Task<DbList<TEntity>> TrackGetDataResultAsync<TEntity>(
            FlattenedGetDataResult<TEntity> response)
            where TEntity : class;
        DataTracker TemporalDataTracker { get; }
        DataTracker OfflineDataTracker { get; }
        Task<bool> ClearOfflineStateAsync();
        Task<bool> SaveOfflineStateAsync();
        Task<bool> RestoreOfflineStateAsync();
        IqlDataChanges GetOfflineChanges(object[] entities = null, IProperty[] properties = null);
        IqlDataChanges GetChanges(object[] entities = null, IProperty[] properties = null);
        IQueuedUpdateEntityOperation[] GetUpdates(object[] entities = null, IProperty[] properties = null);
        IQueuedDeleteEntityOperation[] GetDeletions(object[] entities = null);
        IQueuedAddEntityOperation[] GetAdditions(object[] entities = null);

        T AttachEntity<T>(T entity, bool? cloneIfAttachedElsewhere = null)
            where T : class;
        List<T> AttachEntities<T>(IEnumerable<T> entity, bool? cloneIfAttachedElsewhere = null)
            where T : class;
        bool? IsEntityNew(object entity
#if TypeScript
            , Type entityType = null
#endif
        );
        CompositeKey GetCompositeKey(object entity);
        Task<T> GetEntityAsync<T>(object entity, bool? trackResult = null)
            where T : class;
        /// <summary>
        /// Check for equivalency of two entities or composite keys (can be mixed).
        /// </summary>
        /// <param name="left">Left entity or composite key.</param>
        /// <param name="right">Right entity or composite key.</param>
        /// <returns>Whether the two objects represent database equivalency.</returns>
        bool AreEquivalent(object left, object right);
        Task<IEntityValidationResult> ValidateEntityBaseAsync(object entity);
        Task<EntityValidationResult<T>> ValidateEntityAsync<T>(T entity)
            where T : class
            ;
        Task<IPropertyValidationResult> ValidateEntityPropertyByExpressionAsync<T, TProperty>(object entity,
            Expression<Func<object, TProperty>> property)
            where T : class
            ;
        Task<IPropertyValidationResult> ValidateEntityPropertyByNameAsync(object entity, string property);
        Task<IPropertyValidationResult> ValidateEntityPropertyAsync(object entity, IProperty property);
        bool IsTracked(object entity);
        UsersManager UsersManager { get; }
        CustomReportsManager CustomReportsManager { get; }
        UserSettingsManager UserSettingsManager { get; }
        INestedSetsProviderBase NestedSetsProviderForType(Type type);
        INestedSetsProvider<T> NestedSetsProviderFor<T>();
        void AbandonChanges();
        void AbandonChangesForEntity(object entity);
        void AbandonChangesForEntities(IEnumerable<object> entities);
        void AbandonChangesForEntityState(IEntityStateBase state);
        void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states);
        Task<SaveChangesResult> CommitQueueAsync(IEnumerable<IQueuedOperation> operations);
        bool HasOfflineChanges();
        Task<SaveChangesResult> SaveChangesAsync(IEnumerable<object> entities = null, IEnumerable<IProperty> properties = null);
        Task<SaveChangesResult> SaveOfflineChangesAsync();
        bool TrackEntities { get; set; }
        bool AllowOffline { get; set; }
        string SynchronicityKey { get; set; }
        string OfflineSynchronicityKey { get; set; }
        EvaluateContext EvaluateContext { get; set; }
        IDataStore DataStore { get; }
        EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        void RegisterConfiguration<T>(T configuration)
            where T : class;

        IEntityStateBase GetEntityState(object entity, Type entityType = null);
        T GetConfiguration<T>()
            where T : class;

        IDbQueryable AsDbSetByType(Type entityType);
        DbSet<T, TKey> AsDbSet<T, TKey>() where T : class;
        bool IsIdMatch(object left, object right, Type type);
        bool EntityPropertiesMatch(object entity, CompositeKey key);
        bool EntityHasKey(object left, Type type, CompositeKey key);
        IEntityStateBase DeleteEntity(object entity
#if TypeScript
            , Type entityType = null
#endif
        );
        IEntityStateBase CascadeDeleteEntity(object entity, object cascadedFromEntity, IRelationship relationship
#if TypeScript
            , Type entityType, Type cascadedFromEntityType
#endif
        );
        IEntityStateBase AddEntity(object entity
#if TypeScript
            , Type entityType = null
#endif
        );
        Task<T> RefreshEntity<T>(T entity
#if TypeScript
            , Type entityType = null
#endif
            )
            where T : class
        ;

        Task<T> GetEntityFromEntityAsync<T>(
            T entity
#if TypeScript
            , Type entityType
#endif
            )
            where T : class
        ;

        DbSet<T, TKey> GetDbSet<T, TKey>()
            where T : class;
        DbQueryable<T> GetDbQueryable<T>()
            where T : class;
        TDbSet GetDbSetBySet<TDbSet>()
            where TDbSet : IDbQueryable;
        string GetDbSetPropertyNameBySet<TDbSet>()
            where TDbSet : IDbQueryable;
        string GetDbSetPropertyNameByEntity<T>()
            where T : class;
        string GetDbSetPropertyNameBySetType(Type setType);
        string GetDbSetPropertyNameByEntityType(Type entityType);
        IDbQueryable GetDbSetBySetType(Type entityType);
        IDbQueryable GetDbSetByEntityType(Type entityType);
        IDbQueryable GetDbSetBySetName(string name);
        Task<Dictionary<IProperty, IList>> LoadAllRelationshipsAsync(object entity, LoadRelationshipMode mode = LoadRelationshipMode.Both, Type entityType = null);
        Task<Dictionary<IProperty, IList>> LoadRelationshipsAsync(object entity, IEnumerable<EntityRelationship> relationships, Type entityType = null);
        Task<IList> LoadRelationshipPropertyAsync(object entity, IProperty relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        Task<IList> LoadRelationshipAsync<T>(T entity, Expression<Func<T, object>> relationship, Func<IDbQueryable, IDbQueryable> queryFilter = null);
        //void MarkAsDeletedByKey<TEntity>(CompositeKey entityKey) where TEntity : class;
        //void MarkAsDeletedByKeyAndType(CompositeKey entityKey, Type entityType);
        //void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        Task<GetDataResult<TEntity>> GetAsync<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class;
        Task<SaveChangesResult> ApplySaveChangesAsync(SaveChangesOperation operation);
    }
}