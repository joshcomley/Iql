namespace Iql.Data.Crud.Operations.Results
{
    public class AddEntityResult<T> : EntityCrudResult<T, AddEntityOperation<T>>, IAddEntityResult
        where T : class
    {
        public T RemoteEntity { get; set; }
        object IAddEntityResult.RemoteEntity => RemoteEntity;

        public AddEntityResult(bool success, AddEntityOperation<T> operation) : base(operation.EntityState.Entity, success, operation)
        {
        }
    }
}