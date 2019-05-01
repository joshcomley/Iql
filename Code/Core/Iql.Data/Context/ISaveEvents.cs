using System;
using System.Threading.Tasks;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Events;

namespace Iql.Data.Context
{
    public interface ISaveEvents<TOperation, TResult>
    {
        EventEmitter<TOperation> SavingStarted { get; }
        EventEmitter<TResult> SavingCompleted { get; }
        EventEmitter<TResult> SaveSuccessful { get; }
        AsyncEventEmitter<TOperation> SavingStartedAsync { get; }
        AsyncEventEmitter<TResult> SavingCompletedAsync { get; }
        AsyncEventEmitter<TResult> SaveSuccessfulAsync { get; }
        Task EmitSavingStartedAsync(Func<TOperation> ev);
        Task EmitSavedAsync(Func<TResult> ev);
        Task EmitSavingCompletedAsync(Func<TResult> ev);
    }
}