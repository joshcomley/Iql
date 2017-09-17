using Iql.Queryable.Data;

namespace Iql.Queryable
{
    public static class QueryableExtensions
    {
        public static TQueryResult ToQueryWithAdapter<TQueryResult>
        (
            this IQueryableBase queryable,
            IQueryableAdapter<TQueryResult> adapter,
            IDataContext dataContext
        )
            where TQueryResult : IQueryResultBase
        {
            return (TQueryResult) queryable.ToQueryWithAdapterBase(adapter, dataContext);
        }

        public static IQueryResultBase ToQueryWithAdapterBase
        (
            this IQueryableBase queryable,
            IQueryableAdapterBase adapter,
            IDataContext dataContext
        )
        {
            return new QueryableBaseExtension(queryable).ToQueryWithAdapterBase(adapter, dataContext);
        }
    }
}