using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedUpdateEntityOperation : IQueuedEntityCrudOperation
    {
        new IUpdateEntityOperation Operation { get; }
        new IUpdateEntityResult Result { get; }
    }
}