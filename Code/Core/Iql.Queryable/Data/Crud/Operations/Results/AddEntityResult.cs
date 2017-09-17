namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class AddEntityResult<T> : EntityCrudResult<T, AddEntityOperation<T>>
    {
        public AddEntityResult(bool success, AddEntityOperation<T> operation) : base(success, operation)
        {
        }
    }
}