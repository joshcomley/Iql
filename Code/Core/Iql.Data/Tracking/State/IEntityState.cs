using Iql.Data.Context;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Tracking.State
{
    public interface IEntityState<T> : IEntityStateBase
        where T : class
    {
        new TrackingSet<T> TrackingSet { get; }
        new IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> StatefulSaveEvents { get; }
        new IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> SaveEvents { get; }
        new IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents { get; }
        //new IAsyncEventSubscriber<IqlEntityEvent<T>> SavingAsync { get; }
        //new IAsyncEventSubscriber<IqlEntityEvent<T>> SavedAsync { get; }
        new T Entity { get; }
    }
}