namespace Iql.Data.Crud.Operations
{
    public interface IEntityCrudOperation<T> : IEntityCrudOperationBase
    {
        new T Entity { get; set; }
    }
}