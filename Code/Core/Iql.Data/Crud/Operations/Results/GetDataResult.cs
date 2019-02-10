using Iql.Data.Lists;

namespace Iql.Data.Crud.Operations.Results
{
    public class GetDataResult<T> : DataResult<T, DbList<T>> where T : class
    {
        public bool IsOffline { get; set; }
        public GetDataResult(bool isOffline, DbList<T> data, GetDataOperation<T> operation, bool success) : base(data, operation, success)
        {
        }
    }
}