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
    public interface ITrackingSet : IJsonSerializable
    {
        IDataContext DataContext { get; }
        TrackingSetCollection TrackingSetCollection { get; }
        IDataStore DataStore { get; }
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
        List<IEntityStateBase> TrackEntities(IList data, bool isNew = true, bool allowNew = true, bool onlyMergeWithExisting = false);
        IEntityStateBase TrackEntity(object entity, object mergeWith = null, bool isNew = true, bool onlyMergeWithExisting = false);
        void RemoveEntity(object entity);
        void ResetEntity(object entity);
        void Reset(IEntityStateBase state);
        void ResetAll(List<IEntityStateBase> states);
        IEnumerable<IEntityCrudOperationBase> GetInserts(object[] entities = null);
        IEnumerable<IEntityCrudOperationBase> GetDeletions(object[] entities = null);
        IEnumerable<IUpdateEntityOperation> GetUpdates(object[] entities = null, IProperty[] properties = null);
        void AbandonChanges();
        void AbandonChangesForEntity(object entity);
        void AbandonChangesForEntities(IEnumerable<object> entities);
        void AbandonChangesForEntityState(IEntityStateBase state);
        void AbandonChangesForEntityStates(IEnumerable<IEntityStateBase> states);
    }
}