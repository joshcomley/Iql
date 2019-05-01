using System;
using System.Threading.Tasks;
using Iql.Events;

namespace Iql.Data.Context
{
    public class SaveEvents<TOperation, TResult> : ISaveEvents<TOperation, TResult>
    {
        public ISaveEvents<TOperation, TResult> Global { get; }
        public EventEmitter<TOperation> SavingStarted { get; } = new EventEmitter<TOperation>();
        public EventEmitter<TResult> SavingCompleted { get; } = new EventEmitter<TResult>();
        public EventEmitter<TResult> Saved { get; } = new EventEmitter<TResult>();
        public AsyncEventEmitter<TOperation> SavingStartedAsync { get; } = new AsyncEventEmitter<TOperation>();
        public AsyncEventEmitter<TResult> SavingCompletedAsync { get; } = new AsyncEventEmitter<TResult>();
        public AsyncEventEmitter<TResult> SavedAsync { get; } = new AsyncEventEmitter<TResult>();

        public async Task EmitSavingStartedAsync(Func<TOperation> ev)
        {
            if (Global != null && Global != this)
            {
                await Global.EmitSavingStartedAsync(ev);
            }
            await SavingStartedAsync.EmitAsync(ev);
            SavingStarted.Emit(ev);
        }

        public async Task EmitSavedAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
                await Global.EmitSavedAsync(ev);
            }
            await SavedAsync.EmitAsync(ev);
            Saved.Emit(ev);
        }

        public async Task EmitSavingCompletedAsync(Func<TResult> ev)
        {
            if (Global != null && Global != this)
            {
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