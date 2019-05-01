using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Events;

namespace Iql.Data.Context
{
    public class DataContextSaveEvents<TOperation, TResult> : IDataContextSaveEvents<TOperation, TResult>
        where TOperation : IEntityCrudOperationBase
        where TResult : IEntityCrudResult
    {
        public EventEmitter<TOperation> SavingStarted { get; } = new EventEmitter<TOperation>();
        public EventEmitter<TOperation> SavingCompleted { get; } = new EventEmitter<TOperation>();
        public EventEmitter<TResult> Saved { get; } = new EventEmitter<TResult>();
        public AsyncEventEmitter<TOperation> SavingStartedAsync { get; } = new AsyncEventEmitter<TOperation>();
        public AsyncEventEmitter<TOperation> SavingCompletedAsync { get; } = new AsyncEventEmitter<TOperation>();
        public AsyncEventEmitter<TResult> SavedAsync { get; } = new AsyncEventEmitter<TResult>();
    }
}