namespace Iql.Queryable.Data.Crud.Operations
{
    public interface IEntityCrudOperationBase : IEntitySetCrudOperationBase
    {
        object Entity { get; set; }
    }
}