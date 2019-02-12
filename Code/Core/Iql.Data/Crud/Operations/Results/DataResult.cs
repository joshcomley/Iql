using System.Collections.Generic;
using Iql.Queryable;

namespace Iql.Data.Crud.Operations.Results
{
    public class DataResult<T, TResult> : CrudResult<T, GetDataOperation<T>> where T : class
    {
        public DataResult(TResult data, GetDataOperation<T> operation, bool success, RequestStatus requestStatus = RequestStatus.Online) 
            : base(success, operation, requestStatus)
        {
            Data = data;
        }

        public int? TotalCount { get; set; }
        public TResult Data { get; set; }
        public IList<T> Root { get; set; }
        public IQueryable<T> Queryable { get; set; }
    }
}