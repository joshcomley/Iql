using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.JavaScript.QueryableApplicator
{
    public static class JavaScriptQueryBuilder
    {
        public static string ToJavaScriptQuery(this IDbQueryable queryable)
        {
            return queryable.DataContext.GetJavaScriptQuery(queryable);
        }

        public static string GetJavaScriptQuery(this IDataContext dataContext, IQueryableBase queryable)
        {
            var javaScriptQueryableAdapter = new JavaScriptQueryableAdapter();
            var oDataDataStore = new InMemoryDataStore(javaScriptQueryableAdapter);
            oDataDataStore.DataContext = dataContext;
            var javaScriptQueryResult =
                queryable.ToQueryWithAdapterBase(
                    javaScriptQueryableAdapter,
                    dataContext,
                    null,
                    null) as IJavaScriptQueryResult;
            var queryString = javaScriptQueryResult.ToJavaScriptQuery();
            return queryString;
        }

        public static JavaScriptExpression GetJavaScriptWhereQuery(this IDataContext dataContext, IqlExpression expression)
        {
            var whereQueryExpression = new WhereQueryExpression(null);
            var expressionQueryOperation = new WhereOperation(whereQueryExpression);
            expressionQueryOperation.Expression = expression;
            var jsExpression = JavaScriptQueryableAdapter.GetExpression(
                expressionQueryOperation,
                true,
                dataContext.EntityConfigurationContext);
            return jsExpression;
        }
    }
}