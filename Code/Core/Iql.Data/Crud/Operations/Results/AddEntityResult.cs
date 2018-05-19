namespace Iql.Data.Crud.Operations.Results
{
    public class AddEntityResult<T> : EntityCrudResult<T, AddEntityOperation<T>>, IAddEntityResult
    {
        public T RemoteEntity { get; set; }
        object IAddEntityResult.RemoteEntity => RemoteEntity;

        public AddEntityResult(bool success, AddEntityOperation<T> operation) : base(operation.Entity, success, operation)
        {
        }
    }
}