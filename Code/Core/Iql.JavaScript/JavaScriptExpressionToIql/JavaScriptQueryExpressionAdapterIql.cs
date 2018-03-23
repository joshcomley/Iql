using Iql.JavaScript.JavaScriptExpressionToIql.Parsers;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptQueryExpressionAdapterIql<T> : JavaScriptExpressionAdapter<T, IqlParseResult,
        JavaScriptToIqlExpressionData, IqlExpression>
        where T : class
    {
        public JavaScriptQueryExpressionAdapterIql()
        {
            RegisterParser(() => new BinaryJavaScriptExpressionParserIql<T>());
            RegisterParser(() => new CallJavaScriptExpressionParserIql<T>());
            RegisterParser(() => new MemberJavaScriptExpressionParserIql<T>());
            RegisterParser(() => new LiteralJavaScriptExpressionParserIql<T>());
            RegisterParser(() => new UnaryJavaScriptExpressionParserIql<T>());
            RegisterParser(() => new PropertyIdentifierJavaScriptExpressionParserIql<T>());
        }

        public override JavaScriptToIqlExpressionData NewData()
        {
            return new JavaScriptToIqlExpressionData();
        }
    }
}