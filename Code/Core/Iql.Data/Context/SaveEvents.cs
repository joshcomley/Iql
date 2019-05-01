using System;
using System.Threading.Tasks;
using Iql.Events;

namespace Iql.Data.Context
{
    public class SaveEvents<TOperation, TResult> : SaveEventsBase, ISaveEvents<TOperation, TResult>
    {
        private EventEmitter<TOperation> _savingStarted = null;
        private EventEmitter<TResult> _savingCompleted = null;
        private EventEmitter<TResult> _saveSuccessful = null;
        private AsyncEventEmitter<TOperation> _savingStartedAsync = null;
        private AsyncEventEmitter<TResult> _savingCompletedAsync = null;
        private AsyncEventEmitter<TResult> _saveSuccessfulAsync = null;

        public ISaveEvents<TOperation, TResult> Global { get; }
        public EventEmitter<TOperation> SavingStarted => _savingStarted = _savingStarted ?? new EventEmitter<TOperation>();
        public EventEmitter<TResult> SavingCompleted => _savingCompleted = _savingCompleted ?? new EventEmitter<TResult>();
        public EventEmitter<TResult> SaveSuccessful => _saveSuccessful = _saveSuccessful ?? new EventEmitter<TResult>();
        public AsyncEventEmitter<TOperation> SavingStartedAsync => _savingStartedAsync = _savingStartedAsync ?? new AsyncEventEmitter<TOperation>();
        public AsyncEventEmitter<TResult> SavingCompletedAsync => _savingCompletedAsync = _savingCompletedAsync ?? new AsyncEventEmitter<TResult>();
        public AsyncEventEmitter<TResult> SaveSuccessfulAsync => _saveSuccessfulAsync = _saveSuccessfulAsync ?? new AsyncEventEmitter<TResult>();

        public virtual async Task EmitSavingStartedAsync(Func<TOperation> ev)
        {
            if (Global != null)
            {
                (Global as ISaveEventsInternal)?.Increment();
                await Global.EmitSavingStartedAsync(ev);
            }
            await SavingStartedAsync.EmitAsync(ev);
            SavingStarted.Emit(ev);
        }

        public virtual async Task EmitSavedSuccessfullyAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
                await Global.EmitSavedSuccessfullyAsync(ev);
            }
            await SaveSuccessfulAsync.EmitAsync(ev);
            SaveSuccessful.Emit(ev);
        }

        public virtual async Task EmitSavingCompletedAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
                (Global as ISaveEventsInternal)?.Decrement();
                await Global.EmitSavingCompletedAsync(ev);
            }
            await SavingCompletedAsync.EmitAsync(ev);
            SavingCompleted.Emit(ev);
        }

        public SaveEvents(ISaveEvents<TOperation, TResult> global = null)
        {
            Global = global;
        }
    }
}