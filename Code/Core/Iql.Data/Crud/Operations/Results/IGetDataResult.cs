using Iql.Data.Lists;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IGetDataResult : IDataResult
    {
        object Data { get; }
        bool IsOffline { get; set; }

    }
}