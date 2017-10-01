using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores
{
    public class DataStore : IDataStore
    {
        public TrackingSetCollection Tracking2;

        public IDataContext DataContext { get; set; }
        public List<IQueuedOperation> Queue { get; set; } = new List<IQueuedOperation>();

        public virtual AddEntityResult<TEntity> Add<TEntity>(AddEntityOperation<TEntity> operation)
            where TEntity : class
        {
            GetTracking().GetSet<TEntity>().Track(operation.Entity);
            var result = new AddEntityResult<TEntity>(true, operation);
            Queue.Add(new QueuedAddEntityOperation<TEntity>(
                operation,
                result));
            return result;
        }

        public virtual UpdateEntityResult<TEntity> Update<TEntity>(
            UpdateEntityOperation<TEntity> operation)
            where TEntity : class
        {
            var result = new UpdateEntityResult<TEntity>(true, operation);
            Queue.Add(
                new QueuedUpdateEntityOperation<TEntity>(
                    operation,
                    result));
            return result;
        }

        public virtual DeleteEntityResult<TEntity> Delete<TEntity>(
            DeleteEntityOperation<TEntity> operation)
            where TEntity : class
        {
            var result = new DeleteEntityResult<TEntity>(true, operation);
            Queue.Add(
                new QueuedDeleteEntityOperation<TEntity>(
                    operation,
                    result));
            return result;
        }

        public virtual async Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TrackingSetCollection GetTracking()
        {
            if (Tracking2 == null)
            {
                Tracking2 = new TrackingSetCollection(DataContext);
            }
            return Tracking2;
        }

        public virtual async Task<GetDataResult<TEntity>> Get<TEntity>(GetDataOperation<TEntity> operation)
            where TEntity : class
        {
            var result = new GetDataResult<TEntity>(null, operation, true);
            // perform get and set up tracking on the objects
            var response = await PerformGet(new QueuedGetDataOperation<TEntity>(
                operation,
                result));
            var trackingSet = GetTracking().GetSet<TEntity>();
            trackingSet.Merge(response.Data);
            return result;
        }

        public virtual async Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual async Task<SaveChangesResult> SaveChanges(
            SaveChangesOperation operation)
        {
            GetTracking().Sets.ForEach(trackingSet =>
            {
                trackingSet.GetChanges().ForEach(update =>
                {
                    var updateOperation =
                        Activator.CreateInstance(
                            typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                    Queue.Add((IQueuedOperation)updateOperation);
                    //this.Queue.Add(new QueuedUpdateEntityOperation<object>(update, new UpdateEntityResult<object>(true, update)));
                    //Apply(update);
                });
                trackingSet.Reset();
            });
            //var observable = this.Observable<SaveChangesResult>();
            var saveChangesResult = new SaveChangesResult(false);
            var count = Queue.Count;
            Action decrement = () =>
            {
                count--;
                if (count == 0)
                {
                    //observable.SetData(saveChangesResult);
                }
            };
            var queue = Queue;
            Queue = new List<IQueuedOperation>();
            foreach (var queuedOperation in queue)
            {
                var task = GetType()
                    .GetMethod(nameof(Perform))
                    .MakeGenericMethod(queuedOperation.Operation.EntityType)
                    .Invoke(this, new object[]
                    {
                        queuedOperation, decrement, saveChangesResult
#if TypeScript // The type info
                        ,queuedOperation.Operation.EntityType
#endif
                    }) as Task;
                await task;
            }
            return saveChangesResult;
        }

        //protected ObservableData<T> Observable<T>(Action<ObservableData<T>> fn = null)
        //{
        //    Subscriber<T> resolver = null;
        //    ObservableData<T> result = null;
        //    var observable = new Observable<T>(resolve =>
        //    {
        //        resolver = resolve;
        //        if (result != null && result.DataHasBeenSet())
        //        {
        //            resolve.Next(result.GetData());
        //        }
        //    });
        //    observable.Subscribe();
        //    result = new ObservableData<T>(observable, resolver);
        //    fn?.Invoke(result);
        //    return result;
        //}

        protected int FindEntityIndex<TEntity>(
            Type entityType,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            return Entity.FindIndexOfEntityInSetByKey(
                DataContext,
                clone,
                data
            );
        }

        public virtual async Task Perform<TEntity>(
            IQueuedOperation operation,
            Action decrement,
            SaveChangesResult saveChangesResult) where TEntity : class
        {
            //var ctor: { new(entityType: { new(): any }, success: boolean, entity: any): any };
            ICrudResult result = null;
            switch (operation.Operation.Type)
            {
                case OperationType.Add:
                    var addEntityOperation = (QueuedAddEntityOperation<TEntity>)operation;
                    result = await PerformAdd(addEntityOperation);
                    var remoteEntity = addEntityOperation.Result.RemoteEntity;
                    if (remoteEntity != null)
                    {
                        foreach (var property in remoteEntity.GetType().GetRuntimeProperties())
                        {
                            property.SetValue(addEntityOperation.Operation.Entity, 
                                property.GetValue(remoteEntity));
                        }
                    }
                    GetTracking().GetSet<TEntity>().Track(addEntityOperation.Operation.Entity);
                    break;
                case OperationType.Update:
                    var updateEntityOperation = (QueuedUpdateEntityOperation<TEntity>)operation;
                    result = await PerformUpdate(updateEntityOperation);
                    GetTracking().GetSet<TEntity>().Track(updateEntityOperation.Operation.Entity);
                    break;
                case OperationType.Delete:
                    var deleteEntityOperation = (QueuedDeleteEntityOperation<TEntity>)operation;
                    result = await PerformDelete(deleteEntityOperation);
                    GetTracking().GetSet<TEntity>().Untrack(deleteEntityOperation.Operation.Entity);
                    break;
            }
            saveChangesResult.Results.Add(result as IEntityCrudResult);
            decrement();
        }
    }
}