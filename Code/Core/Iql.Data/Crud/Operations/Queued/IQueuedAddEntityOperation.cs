using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud.Operations.Queued
{
    public interface IQueuedAddEntityOperation : IQueuedEntityCrudOperation
    {
        new IAddEntityOperation Operation { get; }
        new IAddEntityResult Result { get; }
    }
}