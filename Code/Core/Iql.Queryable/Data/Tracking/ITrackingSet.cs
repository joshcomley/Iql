using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking.State;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackingSet
    {
        void SetKey(object entity, Action action);
        bool IsTracked(object entity);
        //void Watch(object entity);
        //void Unwatch(object entity);
        //void SilentlyChangeEntity(object entity, Action action);
        IEntityConfiguration EntityConfiguration { get; }
        IEntityStateBase GetEntityState(object entity);

        IEntityStateBase GetEntityStateByKey(CompositeKey key);
        //IEnumerable<object> TrackedEntites();
        //Type EntityType { get; }
        //object Track(object entity, bool isNew);
        void MarkForDelete(object entity);
        List<IEntityStateBase> TrackEntities(IList data, bool isNew = true, bool allowNew = true);
        IEntityStateBase TrackEntity(object entity, object mergeWith = null, bool isNew = true);
        void RemoveEntity(object entity);
        void ResetEntity(object entity);
        void Reset(IEntityStateBase state);
        void ResetAll(List<IEntityStateBase> states);
        //List<IUpdateEntityOperation> GetChangesInternal(bool reset = false);
        //void Reset();
        //object FindClone(object entity);
        //object FindTrackedEntity(object entity);
        //object FindTrackedEntityByKey(CompositeKey key);
        IEnumerable<IEntityCrudOperationBase> GetInserts();
        IEnumerable<IEntityCrudOperationBase> GetDeletions();
        IEnumerable<IUpdateEntityOperation> GetUpdates();
    }
}