namespace Iql.Data.Crud
{
    public class CrudResultBase : ICrudResult
    {
        public CrudResultBase(bool success)
        {
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