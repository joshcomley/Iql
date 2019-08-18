using System;
using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Events;

namespace Iql.Data.Context
{
    public interface IOperationEvents<TOperation, TResult> : IOperationEventsBase
    {
        new EventEmitter<TOperation> Started { get; }
        new EventEmitter<TResult> Completed { get; }
        new EventEmitter<TResult> Successful { get; }
        new AsyncEventEmitter<TOperation> StartedAsync { get; }
        new AsyncEventEmitter<TResult> CompletedAsync { get; }
        new AsyncEventEmitter<TResult> SuccessfulAsync { get; }
        Task EmitStartedAsync(Func<TOperation> ev);
        Task EmitSuccessAsync(Func<TResult> ev);
        Task EmitCompletedAsync(Func<TResult> ev);
    }

    public interface IOperationEventsBase
    {
        IEventSubscriberBase Started { get; }
        IEventSubscriberBase Completed { get; }
        IEventSubscriberBase Successful { get; }
        IAsyncEventSubscriberBase StartedAsync { get; }
        IAsyncEventSubscriberBase CompletedAsync { get; }
        IAsyncEventSubscriberBase SuccessfulAsync { get; }
        Task EmitStartedAsync(Func<object> ev);
        Task EmitSuccessAsync(Func<object> ev);
        Task EmitCompletedAsync(Func<object> ev);
        void UnsubscribeAll();
    }
}