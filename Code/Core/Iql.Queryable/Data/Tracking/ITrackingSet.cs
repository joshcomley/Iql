using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackingSet
    {
        bool IsTracked(object entity);
        void Watch(object entity);
        void Unwatch(object entity);
        void SilentlyChangeEntity(object entity, Action action);
        void ChangeEntity(object entity, Action action, ChangeEntityMode silently);
        Task ChangeEntityAsync(object entity, Func<Task> action, ChangeEntityMode silently, bool allowAsync = true);
        IEntityStateBase GetEntityState(object entity);
        IEnumerable<object> TrackedEntites();
        Type EntityType { get; }
        object Track(object entity, bool isNew);
        void Untrack(object entity);
        void Merge(IList data, bool isNew);
        object MergeEntity(object entity, bool isNew);
        List<IUpdateEntityOperation> GetChangesInternal(bool reset = false);
        void Reset();
        object FindClone(object entity);
        object FindTrackedEntity(object entity);
        object FindTrackedEntityByKey(CompositeKey key);
        IEnumerable<IEntityCrudOperationBase> GetInserts();
        IEnumerable<IEntityCrudOperationBase> GetDeletions();
        IEnumerable<IUpdateEntityOperation> GetUpdates();
    }
}