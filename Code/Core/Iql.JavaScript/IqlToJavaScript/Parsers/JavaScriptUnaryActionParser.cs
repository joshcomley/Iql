using Iql.Extensions;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptUnaryActionParser : ActionParser<IqlUnaryExpression, JavaScriptIqlData,
        JavaScriptIqlExpressionAdapter>
    {
        public override IqlExpression ToQueryString(IqlUnaryExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
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