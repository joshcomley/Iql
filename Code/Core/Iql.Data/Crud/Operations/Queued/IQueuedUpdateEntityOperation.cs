using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedUpdateEntityOperation : IQueuedOperation
    {
        new IUpdateEntityOperation Operation { get; }
        new IUpdateEntityResult Result { get; }
    }
}