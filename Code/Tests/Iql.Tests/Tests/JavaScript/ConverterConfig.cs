using Iql.Conversion;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
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