using System.Linq;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptLambdaActionParser : JavaScriptActionParserBase<IqlLambdaExpression>
    {
        public override IqlExpression ToQueryString(IqlLambdaExpression action, JavaScriptIqlParserInstance parser)
        {
            var parameters = action.Parameters == null
                ? ""
                : string.Join(", ", action.Parameters.Select(p => parser.Parse(p).ToCodeString()));
            var body = parser.Parse(action.Body).ToCodeString();
            return new IqlFinalExpression<string>($"function({parameters}) {{ return {body}; }}");
        }
    }
}