#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
using Iql.Queryable.Expressions;

#endif

namespace Iql.Tests.Tests.JavaScript
{
    public class ConverterConfig
    {
        public static void Init()
        {
#if TypeScript
            IqlExpressionConversion.DefaultExpressionConverter = () => new JavaScriptExpressionConverter();
#else
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
#endif
        }
    }
}