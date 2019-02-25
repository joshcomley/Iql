using Iql.Data.Lists;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IGetDataResult : IQueryableDataResult
    {
        object Data { get; }
        bool IsOffline { get; set; }
    }
}