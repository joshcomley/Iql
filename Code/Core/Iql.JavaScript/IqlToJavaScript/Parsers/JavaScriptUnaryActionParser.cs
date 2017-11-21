using Iql.Extensions;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptUnaryActionParser : JavaScriptActionParserBase<IqlUnaryExpression>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            JavaScriptIqlParserInstance parser)
        {
            return new IqlAggregateExpression(
                new IqlFinalExpression(ResolveOperator(action)),
                new IqlLiteralExpression(
                    action.Value,
                    (action.Value == null ? typeof(string) : action.Value.GetType()).ToIqlType()
                )
            );
        }

        public string ResolveOperator(IqlUnaryExpression action)
        {
            switch (action.Type)
            {
                case IqlExpressionType.UnarySubtract:
                    return "-";
            }
            JavaScriptErrors.OperationNotSupported(action.Type);
            return null;
        }
    }
}