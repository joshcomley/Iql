namespace Iql.Queryable.Data.Crud.Operations
{
    public interface IUpdateEntityOperation : IEntityCrudOperationBase
    {
        EntityState EntityState { get; }
    }
}