namespace Iql.Data.Crud.Operations.Results
{
    public interface IUpdateEntityResult : IEntityCrudResult
    {
        IUpdateEntityOperation Operation { get; }
    }
}