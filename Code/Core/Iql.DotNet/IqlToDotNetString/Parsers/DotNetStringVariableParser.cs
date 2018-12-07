namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringVariableParser : DotNetStringActionParserBase<IqlVariableExpression>
    {
        public override IqlExpression ToQueryString(IqlVariableExpression action,
            DotNetStringIqlParserContext parser)
        {
            return new IqlFinalExpression<string>(parser.IsParameterName(action.VariableName) ? parser.GetRootEntityParameterName(action.VariableName) : action.VariableName);
        }
    }
}