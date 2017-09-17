using System.Collections.Generic;

namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class GetDataResult<T> : DataResult<T, List<T>> where T : class
    {
        public GetDataResult(List<T> data, GetDataOperation<T> operation, bool success) : base(data, operation, success)
        {
        }
    }
}