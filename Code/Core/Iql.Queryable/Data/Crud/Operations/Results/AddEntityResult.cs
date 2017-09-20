namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class AddEntityResult<T> : EntityCrudResult<T, AddEntityOperation<T>>
    {
        public T RemoteEntity { get; set; }
        public AddEntityResult(bool success, AddEntityOperation<T> operation) : base(success, operation)
        {
        }
    }
}