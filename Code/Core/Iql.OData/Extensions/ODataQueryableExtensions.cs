using Iql.OData.Data;
using Iql.Queryable;
using Iql.Queryable.Data;

namespace Iql.OData.Extensions
{
    public static class ODataQueryableExtensions
    {
        public static string ResolveODataQueryUri<TEntity>(this DbQueryable<TEntity> queryable)
            where TEntity : class
        {
            return queryable.ResolveODataQueryUriFromQuery(queryable.DataContext);
        }

        public static string ResolveODataQueryUriFromQuery(this IQueryableBase queryable, IDataContext dataContext)
        {
            var oDataDataStore = new ODataDataStore();
            oDataDataStore.DataContext = dataContext;
            return oDataDataStore.ResolveODataQueryUri(queryable);
        }
    }
}