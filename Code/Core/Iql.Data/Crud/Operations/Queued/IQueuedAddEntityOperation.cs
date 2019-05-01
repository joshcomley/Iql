using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedAddEntityOperation : IQueuedCrudOperation
    {
        new IAddEntityOperation Operation { get; }
        new IAddEntityResult Result { get; }
    }
}