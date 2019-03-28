using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Parsing.Types;
using Iql.Queryable;

namespace Iql.OData.Extensions
{
    public static class ODataUri
    {
        public static ODataConfiguration ODataConfiguration(this IDataContext dataContext)
        {
            return dataContext.GetConfiguration<ODataConfiguration>();
        }

        public static Task<string> ResolveODataUriAsync(this IDbQueryable queryable)
        {
            return queryable.ResolveODataUriFromQueryAsync();
        }

        public static async Task<string> ResolveODataUriFromQueryAsync(this IQueryableBase queryable, ITypeResolver typeResolver = null, ODataConfiguration oDataConfiguration = null)
        {
            if (oDataConfiguration == null && queryable is IDbQueryable)
            {
                var dbQueryable = queryable as IDbQueryable;
                var dataContext = dbQueryable.DataContext;
                typeResolver = typeResolver ?? dataContext.EntityConfigurationContext;
                oDataConfiguration = dataContext.ODataConfiguration();
            }
            var iql = await queryable.ToIqlAsync();
            var expressionConverter = new ODataExpressionConverter(oDataConfiguration);
            var expressionString = expressionConverter.ConvertIqlToExpressionStringByType(iql, typeResolver, queryable.ItemType);
            return expressionString;
        }
    }
}