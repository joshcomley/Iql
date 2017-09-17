namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class DataResult<T, TResult> : CrudResult<T, GetDataOperation<T>> where T : class
    {
        public DataResult(TResult data, GetDataOperation<T> operation, bool success) : base(success, operation)
        {
            Data = data;
        }

        public TResult Data { get; set; }
    }
}