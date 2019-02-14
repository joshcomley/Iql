using Iql.Data.Lists;

namespace Iql.Data.Crud.Operations.Results
{
    public class GetDataResult<T> : DataResult<T, DbList<T>>, IGetDataResult
        where T : class
    {
        object IGetDataResult.Data => Data;
        public bool IsOffline { get; set; }

        public GetDataResult(bool isOffline, DbList<T> data, GetDataOperation<T> operation, bool success) : base(data,
            operation, success)
        {
            IsOffline = isOffline;
        }
    }
}