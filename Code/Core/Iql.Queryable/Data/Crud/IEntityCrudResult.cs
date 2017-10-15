using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data.Crud
{
    public interface IEntityCrudResult
    {
        OperationType Type { get; }
        object LocalEntity { get; }
    }
}