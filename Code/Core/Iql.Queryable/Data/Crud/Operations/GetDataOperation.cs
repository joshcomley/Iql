namespace Iql.Queryable.Data.Crud.Operations
{
    public class GetDataOperation<T> : EntitySetCrudOperation<T> where T : class
    {
        public GetDataOperation(IQueryable<T> queryable, IDataContext dataContext) : base(OperationType.Get,
            dataContext)
        {
            Queryable = queryable;
        }

        public IQueryable<T> Queryable { get; set; }
    }
}