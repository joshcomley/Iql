using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public class CrudResultBase : ICrudResult
    {
        public RequestStatus RequestStatus { get; set; }
        public CrudResultBase(bool success, RequestStatus requestStatus = RequestStatus.Online)
        {
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