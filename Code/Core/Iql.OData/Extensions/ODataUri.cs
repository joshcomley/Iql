using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Queryable;

namespace Iql.OData.Extensions
{
    public static class ODataUri
    {
        public static string ResolveODataUri(this IDbQueryable queryable)
        {
            return queryable.ResolveODataUriFromQuery(queryable.DataContext);
        }

        public static string ResolveODataUriFromQuery(this IQueryableBase queryable, IDataContext dataContext)
        {
            var oDataDataStore = new ODataDataStore();
            oDataDataStore.DataContext = dataContext;
            return oDataDataStore.ResolveODataQueryUri(queryable);
        }
    }
}