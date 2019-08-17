using System;
using System.Threading.Tasks;
using Iql.Events;

namespace Iql.Data.Context
{
    public class OperationEvents<TOperation, TResult> : OperationEventsBase, IOperationEvents<TOperation, TResult>
    {
        private EventEmitter<TOperation> _savingStarted = null;
        private EventEmitter<TResult> _savingCompleted = null;
        private EventEmitter<TResult> _saveSuccessful = null;
        private AsyncEventEmitter<TOperation> _savingStartedAsync = null;
        private AsyncEventEmitter<TResult> _savingCompletedAsync = null;
        private AsyncEventEmitter<TResult> _saveSuccessfulAsync = null;

        public IOperationEvents<TOperation, TResult> Global { get; }
        public EventEmitter<TOperation> Started => _savingStarted = _savingStarted ?? new EventEmitter<TOperation>();
        IEventSubscriberBase IOperationEventsBase.Started => Started;
        public EventEmitter<TResult> Completed => _savingCompleted = _savingCompleted ?? new EventEmitter<TResult>();
        IEventSubscriberBase IOperationEventsBase.Completed => Completed;
        public EventEmitter<TResult> Successful => _saveSuccessful = _saveSuccessful ?? new EventEmitter<TResult>();
        IEventSubscriberBase IOperationEventsBase.Successful => Successful;
        public AsyncEventEmitter<TOperation> StartedAsync => _savingStartedAsync = _savingStartedAsync ?? new AsyncEventEmitter<TOperation>();
        IAsyncEventSubscriberBase IOperationEventsBase.StartedAsync => StartedAsync;
        public AsyncEventEmitter<TResult> CompletedAsync => _savingCompletedAsync = _savingCompletedAsync ?? new AsyncEventEmitter<TResult>();
        IAsyncEventSubscriberBase IOperationEventsBase.CompletedAsync => CompletedAsync;
        public AsyncEventEmitter<TResult> SuccessfulAsync => _saveSuccessfulAsync = _saveSuccessfulAsync ?? new AsyncEventEmitter<TResult>();
        IAsyncEventSubscriberBase IOperationEventsBase.SuccessfulAsync => SuccessfulAsync;

        public virtual async Task EmitStartedAsync(Func<TOperation> ev)
        {
            if (Global != null)
            {
                (Global as ISaveEventsInternal)?.Increment();
                await Global.EmitStartedAsync(ev);
            }
            await StartedAsync.EmitAsync(ev);
            Started.Emit(ev);
        }

        Task IOperationEventsBase.EmitStartedAsync(Func<object> ev)
        {
            return EmitStartedAsync(() => (TOperation)ev());
        }

        public virtual async Task EmitSuccessAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
                await Global.EmitSuccessAsync(ev);
            }
            await SuccessfulAsync.EmitAsync(ev);
            Successful.Emit(ev);
        }

        Task IOperationEventsBase.EmitSuccessAsync(Func<object> ev)
        {
            return EmitSuccessAsync(() => (TResult)ev());
        }

        public virtual async Task EmitCompletedAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
                (Global as ISaveEventsInternal)?.Decrement();
                await Global.EmitCompletedAsync(ev);
            }
            await CompletedAsync.EmitAsync(ev);
            Completed.Emit(ev);
        }

        public void UnsubscribeAll()
        {
            foreach (var sub in new IEventUnsubscriber[] { Started, Completed, Successful, StartedAsync, CompletedAsync, SuccessfulAsync })
            {
                sub.UnsubscribeAll();
            }
        }

        Task IOperationEventsBase.EmitCompletedAsync(Func<object> ev)
        {
            return EmitCompletedAsync(() => (TResult)ev());
        }

        public OperationEvents(IOperationEvents<TOperation, TResult> global = null)
        {
            Global = global;
        }
    }
}