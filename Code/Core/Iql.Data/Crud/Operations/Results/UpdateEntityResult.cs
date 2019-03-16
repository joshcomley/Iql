namespace Iql.Data.Crud.Operations.Results
{
    public class UpdateEntityResult<T> : EntityCrudResult<T, UpdateEntityOperation<T>>, IUpdateEntityResult
    {
        IUpdateEntityOperation IUpdateEntityResult.Operation => Operation;

        public UpdateEntityResult(bool success, UpdateEntityOperation<T> operation)
            : base(operation.Entity, success, operation)
        {
        }
    }
}