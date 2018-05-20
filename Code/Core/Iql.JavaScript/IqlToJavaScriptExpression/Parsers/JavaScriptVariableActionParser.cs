namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptVariableActionParser : JavaScriptActionParserBase<IqlVariableExpression>
    {
        public override IqlExpression ToQueryString(IqlVariableExpression action, JavaScriptIqlParserInstance parser)
        {
            return new IqlFinalExpression<string>(parser.IsParameterName(action.VariableName) ? parser.GetRootEntityParameterName(action.VariableName) : action.VariableName);
        }
    }
}