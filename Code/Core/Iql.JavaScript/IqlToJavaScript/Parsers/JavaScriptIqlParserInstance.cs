using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptIqlParserInstance : ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter, JavaScriptOutput>
    {
        public JavaScriptIqlParserInstance(JavaScriptIqlExpressionAdapter adapter) : base(adapter)
        {
        }

        public override JavaScriptOutput Parse(IqlExpression expression)
        {
            return new JavaScriptOutput(ParseAsString(expression));
        }
    }
}