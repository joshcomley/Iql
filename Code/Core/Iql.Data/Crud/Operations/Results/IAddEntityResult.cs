namespace Iql.Data.Crud.Operations.Results
{
    public interface IAddEntityResult : IEntityCrudResult
    {
        object RemoteEntity { get; }
    }
}