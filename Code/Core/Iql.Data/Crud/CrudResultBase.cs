using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public abstract class CrudResultBase : ICrudResult
    {
        private ICrudOperation _operation;
        ICrudOperation ICrudResult.Operation => _operation;
        public RequestStatus RequestStatus { get; set; }
        public CrudResultBase(
            ICrudOperation operation,
            bool success, 
            RequestStatus requestStatus = RequestStatus.Online)
        {
            _operation = operation;
            RequestStatus = requestStatus;
            if (RequestStatus != RequestStatus.Online && RequestStatus != RequestStatus.Offline)
            {
                RequestStatus = RequestStatus.Online;
            }
            Success = success;
        }

        public virtual bool Success { get; set; }
    }


    //public class EntityCrudOperation : CrudOperationBase
    //{
    //    public EntityCrudOperation(IDataContext dataContext)
    //        : base(dataContext)
    //    {
    //    }
    //}
}