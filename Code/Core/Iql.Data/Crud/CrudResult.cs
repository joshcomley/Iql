using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public class CrudResult<TResult, TOperation> : TypedCrudResult
        where TOperation : IEntitySetCrudOperationBase
    {
        public CrudResult(bool success, TOperation operation, RequestStatus requestStatus = RequestStatus.Online)
            : base(typeof(TResult), success, requestStatus)
        {
            Operation = operation;
        }

        public TOperation Operation { get; }
    }
}