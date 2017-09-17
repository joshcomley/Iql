using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data.Crud
{
    public class CrudResult<TFunky, TOperation> : TypedCrudResult
        where TOperation : IEntitySetCrudOperationBase
    {
        public CrudResult(bool success, TOperation operation)
            : base(typeof(TFunky), success)
        {
            Operation = operation;
        }

        public TOperation Operation { get; }
    }
}