using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackingSet
    {
        void Watch(object entity);
        void Unwatch(object entity);
        void SilentlyChangeEntity(object entity, Action action);
        void ChangeEntity(object entity, Action action, ChangeEntityMode silently);
        IEntityStateBase GetEntityState(object entity);
        void EnsureIntegrity();
        IEnumerable<object> TrackedEntites();
        Type EntityType { get; }
        object Track(object entity, bool isNew);
        void Untrack(object entity);
        void Merge(IList data, bool isNew);
        object MergeEntity(object entity, bool isNew);
        List<IUpdateEntityOperation> GetChangesInternal(bool reset = false);
        void Reset();
        object FindClone(object entity);
        ITrackedEntity FindTrackedEntity(object entity);
        ITrackedEntity FindTrackedEntityByKey(CompositeKey key);
        ITrackedEntity FindEntity(object entity);
        ITrackedEntity FindEntityByKey(CompositeKey entity);
        IEnumerable<ITrackedRelationship> FindRelationships(object entity, CompositeKey key);
        IEnumerable<IEntityCrudOperationBase> GetInserts();
        IEnumerable<IEntityCrudOperationBase> GetDeletions();
        IEnumerable<IUpdateEntityOperation> GetUpdates();
    }
}