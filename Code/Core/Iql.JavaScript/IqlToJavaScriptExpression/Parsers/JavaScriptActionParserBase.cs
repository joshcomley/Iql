using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptActionParserBase<TIqlExpression> :
        ActionParser<TIqlExpression,
            JavaScriptIqlData,
            JavaScriptIqlExpressionAdapter,
            string,
            JavaScriptOutput,
            JavaScriptIqlParserContext,
            JavaScriptExpressionConverter>
        where TIqlExpression : IqlExpression { }
}