namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class GetDataResult<T> : DataResult<T, DbList<T>> where T : class, IEntity
    {
        public GetDataResult(DbList<T> data, GetDataOperation<T> operation, bool success) : base(data, operation, success)
        {
        }
    }
}