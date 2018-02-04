using Iql.Queryable.Data;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public static class QueryableExtensions
    {
        public static TQueryResult ToQueryWithAdapter<TQueryResult, TQueryAdapter>
        (
            this IQueryableBase queryable,
            TQueryAdapter adapter,
            IDataContext dataContext,
            IQueryOperationContextBase parentContext,
            IQueryResultBase parentResult
        )
            where TQueryResult : IQueryResultBase
            where TQueryAdapter : IQueryableAdapter<TQueryResult, TQueryAdapter>
        {
            return (TQueryResult) queryable
                .ToQueryWithAdapterBase(
                    adapter,
                    dataContext,
                    parentContext,
                    parentResult);
        }

        public static IQueryResultBase ToQueryWithAdapterBase
        (
            this IQueryableBase queryable,
            IQueryableAdapterBase adapter,
            IDataContext dataContext,
            IQueryOperationContextBase parentContext,
            IQueryResultBase parentResult
        )
        {
            return new QueryableBaseExtension(queryable)
                .ToQueryWithAdapterBase(
                    adapter,
                    dataContext,
                    parentContext,
                    parentResult);
        }
    }
}