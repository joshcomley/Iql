using System;
using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Events;

namespace Iql.Data.Context
{
    public class DataContextSaveEvents<TOperation, TResult> : IDataContextSaveEvents<TOperation, TResult>
    {
        public EventEmitter<TOperation> SavingStarted { get; } = new EventEmitter<TOperation>();
        public EventEmitter<TResult> SavingCompleted { get; } = new EventEmitter<TResult>();
        public EventEmitter<TResult> Saved { get; } = new EventEmitter<TResult>();
        public AsyncEventEmitter<TOperation> SavingStartedAsync { get; } = new AsyncEventEmitter<TOperation>();
        public AsyncEventEmitter<TResult> SavingCompletedAsync { get; } = new AsyncEventEmitter<TResult>();
        public AsyncEventEmitter<TResult> SavedAsync { get; } = new AsyncEventEmitter<TResult>();

        public async Task EmitSavingStartedAsync(Func<TOperation> ev)
        {
            await SavingStartedAsync.EmitAsync(ev);
            SavingStarted.Emit(ev);
        }

        public async Task EmitSavedAsync(Func<TResult> ev)
        {
            await SavedAsync.EmitAsync(ev);
            Saved.Emit(ev);
        }

        public async Task EmitSavingCompletedAsync(Func<TResult> ev)
        {
            await SavingCompletedAsync.EmitAsync(ev);
            SavingCompleted.Emit(ev);
        }
    }
}