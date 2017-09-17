namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class GetSingleResult<T> : DataResult<T, T> where T : class
    {
        public GetSingleResult(T data, GetDataOperation<T> operation, bool success) : base(data, operation, success)
        {
        }
    }
}