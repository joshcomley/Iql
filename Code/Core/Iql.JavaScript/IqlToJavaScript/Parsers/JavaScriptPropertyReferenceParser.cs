using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptPropertyReferenceParser : ActionParser<IqlPropertyExpression, JavaScriptIqlData,
        JavaScriptIqlExpressionAdapter>
    {
        protected string Separator = ".";

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            var separator = new IqlFinalExpression("");
            IqlExpression parent = null;
            if (action.Parent != null && (action.Parent.Type != IqlExpressionType.RootReference ||
                                          !string.IsNullOrWhiteSpace(parser.Adapter.RootVariableName)))
            {
                parent = action.Parent;
                separator = new IqlFinalExpression(Separator);
            }
            var accessorExpression = new IqlAggregateExpression(
                parent,
                separator,
                new IqlFinalExpression(action.PropertyName)
            );
            if (parser.IsFilter)
            {
                if (parent != null)
                {
                    return
                        new IqlParenthesisExpression(
                            new IqlAggregateExpression(
                                new IqlAndExpression(
                                    parent,
                                    accessorExpression
                                ),
                                new IqlFinalExpression(" ? "),
                                accessorExpression,
                                new IqlFinalExpression(" : "),
                                new IqlFinalExpression("null")
                            )
                        );
                }
                return new IqlFinalExpression(action.PropertyName);
            }
            return accessorExpression;
        }
    }
}