using System.Collections;
using System.Collections.Generic;
using Iql.Data.Lists;
using Iql.Queryable;

namespace Iql.Data.Crud.Operations.Results
{
    public class DataResult<T, TResult> : CrudResult<T, GetDataOperation<T>>, IDataResult
        where T : class
    {
        public DataResult(TResult data, GetDataOperation<T> operation, bool success, RequestStatus requestStatus = RequestStatus.Online) 
            : base(success, operation, requestStatus)
        {
            Data = data;
        }

        public int? TotalCount { get; set; }
        public TResult Data { get; set; }
        public IList<T> Root { get; set; }
        IList IDataResult.Root
        {
            get => (IList) Root;
            set => Root = (IList<T>) value;
        }
        public IQueryable<T> Queryable { get; set; }
        IQueryableBase IDataResult.Queryable
        {
            get => Queryable;
            set => Queryable = (IQueryable<T>)value;
        }
    }
}