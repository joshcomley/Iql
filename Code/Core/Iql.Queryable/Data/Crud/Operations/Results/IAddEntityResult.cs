namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public interface IAddEntityResult : IEntityCrudResult
    {
        object RemoteEntity { get; }
    }
}