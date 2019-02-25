namespace Iql.Data.Crud.Operations.Results
{
    public interface IAggregatedGetDataResult : IDataResult
    {
        IGetDataResult[] Results { get; }
    }
}