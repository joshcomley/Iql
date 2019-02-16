using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores;
using Iql.Data.Tracking.State;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public interface ITrackingSet : IJsonSerializable, IDataChangeProvider
    {
        void RemoveEntityByKey(CompositeKey compositeKey);
        IEntityStateBase AddEntity(object entity);
        IEntityStateBase Synchronise(object entity, bool overrideChanges, bool isRemote);
        IEntityStateBase AttachEntity(object entity, bool isLocal);
        ITrackingSet Merge(object localEntity, object remoteEntity, bool overrideChanges, bool isRemote);
        IDataContext DataContext { get; }
        DataTracker DataTracker { get; }
        void SetKey(object entity, Action action);
        bool IsTracked(object entity);
        bool IsMatchingEntityTracked(object entity);
        bool DifferentEntityWithSameKeyIsTracked(object entity);
        IEntityConfiguration EntityConfiguration { get; }
        IEntityStateBase GetEntityState(object entity);
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
        void AbandonChanges();
        void AbandonChangesForEntity(object entity);
        void AbandonChangesForEntities(IEnumerable<object> entities);
        void AbandonChangesForEntityState(IEntityStateBase state);
        void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states);
        void Clear();
    }
}