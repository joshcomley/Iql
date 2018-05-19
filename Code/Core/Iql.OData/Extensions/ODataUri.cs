using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Queryable;

namespace Iql.OData.Extensions
{
    public static class ODataUri
    {
        public static ODataConfiguration ODataConfiguration(this IDataContext dataContext)
        {
            return dataContext.GetConfiguration<ODataConfiguration>();
        }

        public static async Task<string> ResolveODataUriAsync(this IDbQueryable queryable)
        {            
            return await queryable.ResolveODataUriFromQueryAsync();
        }

        public static async Task<string> ResolveODataUriFromQueryAsync(this IQueryableBase queryable, ODataConfiguration oDataConfiguration = null)
        {
            if (oDataConfiguration == null && queryable is IDbQueryable)
            {
                var dbQueryable = queryable as IDbQueryable;
                var dataContext = dbQueryable.DataContext;
                oDataConfiguration = dataContext.ODataConfiguration();
            }
            var iql = await queryable.ToIqlAsync();
            var expressionConverter = new ODataExpressionConverter(oDataConfiguration);
            var expressionString = expressionConverter.ConvertIqlToExpressionStringByType(iql, queryable.ItemType);
            return expressionString;
        }
    }
}