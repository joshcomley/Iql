using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IGetSingleResult : IQueryableDataResult
    {
        IEntityStateBase EntityState { get; }
        object Entity { get; }
    }
}