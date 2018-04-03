#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;

namespace Iql.Tests.Tests.JavaScript
{
    public class ConverterConfig
    {
        public static void Init()
        {
#if TypeScript
            IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionConverter();
#else
            IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
#endif
        }
    }
}