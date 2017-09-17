namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class DeleteEntityResult<T> : EntityCrudResult<T, DeleteEntityOperation<T>>
    {
        public DeleteEntityResult(bool success, DeleteEntityOperation<T> operation) : base(success, operation)
        {
        }
    }
}