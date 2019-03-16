using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedDeleteEntityOperation  : IQueuedOperation
    {
        new IDeleteEntityOperation Operation { get; }
        new IDeleteEntityResult Result { get; }
    }
}