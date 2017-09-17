namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class UpdateEntityResult<T> : EntityCrudResult<T, UpdateEntityOperation<T>>
    {
        public UpdateEntityResult(bool success, UpdateEntityOperation<T> operation)
            : base(success, operation)
        {
        }
    }
}