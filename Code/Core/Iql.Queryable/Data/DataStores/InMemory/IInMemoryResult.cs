using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public interface IInMemoryResult : IQueryResultBase
    {
        InMemoryQueryResult GetResults();
    }
}