using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptActionParserBase<TIqlExpression> :
        ActionParser<TIqlExpression, JavaScriptIqlData, JavaScriptIqlExpressionAdapter,
            string,
            JavaScriptOutput, JavaScriptIqlParserInstance>
        where TIqlExpression : IqlExpression
    {
    }
}