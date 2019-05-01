using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Events;

namespace Iql.Data.Context
{
    public interface IDataContextSaveEvents<TOperation, TResult>
        where TOperation : IEntityCrudOperationBase
        where TResult : IEntityCrudResult
    {
        EventEmitter<TOperation> SavingStarted { get; }
        EventEmitter<TOperation> SavingCompleted { get; }
        EventEmitter<TResult> Saved { get; }
        AsyncEventEmitter<TOperation> SavingStartedAsync { get; }
        AsyncEventEmitter<TOperation> SavingCompletedAsync { get; }
        AsyncEventEmitter<TResult> SavedAsync { get; }
    }
}