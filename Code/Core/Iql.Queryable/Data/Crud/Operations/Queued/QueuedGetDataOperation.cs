using Iql.Queryable.Data.Crud.Operations.Results;

namespace Iql.Queryable.Data.Crud.Operations.Queued
{
    public class QueuedGetDataOperation<T> : QueuedOperation<GetDataOperation<T>, GetDataResult<T>> where T : class
    {
        public QueuedGetDataOperation(GetDataOperation<T> operation, GetDataResult<T> result) : base(QueuedOperationType.Get, operation, result)
        {
        }
    }
}