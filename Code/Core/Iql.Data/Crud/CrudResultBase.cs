using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public class CrudResultBase : ICrudResult
    {
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Online;
        public CrudResultBase(bool success, RequestStatus requestStatus = RequestStatus.Online)
        {
            RequestStatus = requestStatus;
            Success = success;
        }

        public bool Success { get; set; }
    }


    //public class EntityCrudOperation : CrudOperationBase
    //{
    //    public EntityCrudOperation(IDataContext dataContext)
    //        : base(dataContext)
    //    {
    //    }
    //}
}