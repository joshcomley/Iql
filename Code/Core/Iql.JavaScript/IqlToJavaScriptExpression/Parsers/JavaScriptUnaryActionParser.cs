using Iql.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptUnaryActionParser : JavaScriptActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression<string>(ResolveOperator(action)),
                new IqlLiteralExpression(
                    action.Value,
                    (action.Value == null ? typeof(string) : action.Value.GetType()).ToIqlType()
                )
            );
        }

        public string ResolveOperator(IqlUnaryExpression action)
        {
            switch (action.Kind)
            {
                case IqlExpressionKind.UnarySubtract:
                    return "-";
            }
            JavaScriptErrors.OperationNotSupported(action.Kind);
            return null;
        }
    }
}