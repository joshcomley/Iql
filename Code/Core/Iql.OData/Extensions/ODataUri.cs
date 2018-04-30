using System.Threading.Tasks;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;

namespace Iql.OData.Extensions
{
    public static class ODataUri
    {
        public static async Task<string> ResolveODataUriAsync(this IDbQueryable queryable)
        {
            return await queryable.ResolveODataUriFromQueryAsync(queryable.DataContext);
        }

        public static async Task<string> ResolveODataUriFromQueryAsync(this IQueryableBase queryable, IDataContext dataContext)
        {
            var oDataDataStore = new ODataDataStore();
            oDataDataStore.DataContext = dataContext;
            return await oDataDataStore.ResolveODataQueryUriAsync(queryable);
        }
    }
}