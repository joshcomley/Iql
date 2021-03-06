using System;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public interface ITrackingSet : IJsonSerializable, IDataChangeProvider, IDisposable
    {
        bool LiveTracking { get; }
        ITrackingSet SetMarkedForDeletion(IEntityStateBase state, bool isMarkedForDeletion);
        IEntityStateBase[] GetChangedStates();
        IEntityStateBase[] AllEntityStates();
        Type EntityType { get; }
        IEntityStateBase Restore(SerializedEntityState entityState);
        void RemoveEntityByKey(CompositeKey compositeKey);
        Func<IEntityStateBase> AddEntity(object entity);
        object Synchronise(object remoteEntity, bool overrideChanges, bool isRemote, object existingEntity);
        Func<IEntityStateBase> AttachEntity(object entity, bool isLocal);
        void NotifyEntityIsNewChanged(IEntityStateBase entityStateBase);
        ITrackingSet Merge(object localEntity, object remoteEntity, bool overrideChanges, bool isRemote);
        DataTracker DataTracker { get; }
        void SetKey(object entity, Action action);
        bool IsTracked(object entity);
        bool IsMatchingEntityTracked(object entity);
        bool IsEntityTracked(object entity);
        bool DifferentEntityWithSameKeyIsTracked(object entity);
        IEntityConfiguration EntityConfiguration { get; }
        IEntityStateBase FindEntityState(object entity);
        IEntityStateBase GetEntityState(object entity);
        object GetEntityByKey(CompositeKey entity);
        IEntityStateBase FindMatchingEntityState(object entity);
        IEntityStateBase GetEntityStateByKey(CompositeKey key);
        bool KeyIsTracked(CompositeKey key);
        void MarkForDelete(object entity);
        //List<IEntityStateBase> TrackEntities(IList data, bool isNew = true, bool allowNew = true, bool onlyMergeWithExisting = false);
        //IEntityStateBase AttachEntity(object entity, object mergeWith = null, bool isNew = true, bool onlyMergeWithExisting = false);
        void RemoveEntity(object entity);
        void HardResetEntity(object entity);
        void HardReset(IEntityStateBase state);
        void SoftResetEntity(object entity, bool markAsNotNew);
        void SoftReset(IEntityStateBase state, bool markAsNotNew);
        void HardResetAll(List<IEntityStateBase> states);
        void SoftResetAll(List<IEntityStateBase> states, bool markAsNotNew);
        void AbandonChanges(object[] entities = null, IProperty[] properties = null);
        void AbandonChangesForEntity(object entity);
        void AbandonChangesForEntities(IEnumerable<object> entities);
        void AbandonChangesForEntityState(IEntityStateBase state);
        void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states, IProperty[] properties = null);
        void Clear();
        void NotifySaveApplied(object[] entities, IProperty[] properties, List<IEntityStateBase> failedEntitySaves);
        bool IsNew(object entity);
    }
}