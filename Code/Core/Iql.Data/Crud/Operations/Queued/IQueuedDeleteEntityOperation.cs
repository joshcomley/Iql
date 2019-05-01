using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedDeleteEntityOperation  : IQueuedCrudOperation
    {
        new IDeleteEntityOperation Operation { get; }
        new IDeleteEntityResult Result { get; }
    }
}