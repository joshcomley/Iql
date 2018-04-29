namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptAnyAllActionParser : JavaScriptActionParserBase<IqlAnyAllExpression>
    {
        public override IqlExpression ToQueryString(
            IqlAnyAllExpression action,
            JavaScriptIqlParserInstance parser)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.Any:
                    return new IqlParenthesisExpression(
                        new IqlAggregateExpression(
                            action.Parent,
                            new IqlFinalExpression<string>($".filter(function({parser.GetRootEntityParameterName(action.RootVariableName)}) {{ return "),
                            action.Value,
                            new IqlFinalExpression<string>("; }).length > 0")
                        )
                    );
                case IqlExpressionKind.All:
                    break;
            }
            return action;
        }
    }
}