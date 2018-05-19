namespace Iql.Data.Crud.Operations.Results
{
    public class DeleteEntityResult<T> : EntityCrudResult<T, DeleteEntityOperation<T>>, IDeleteEntityResult
    {
        public DeleteEntityResult(bool success, DeleteEntityOperation<T> operation) : base(operation.Entity, success, operation)
        {
        }
    }
}