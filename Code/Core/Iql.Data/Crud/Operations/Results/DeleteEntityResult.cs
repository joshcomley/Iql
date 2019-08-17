namespace Iql.Data.Crud.Operations.Results
{
    public class DeleteEntityResult<T> : EntityCrudResult<T, DeleteEntityOperation<T>>, IDeleteEntityResult
        where T : class
    {
        public DeleteEntityResult(bool success, DeleteEntityOperation<T> operation) : base(operation.EntityState.Entity, success, operation)
        {
        }
    }
}