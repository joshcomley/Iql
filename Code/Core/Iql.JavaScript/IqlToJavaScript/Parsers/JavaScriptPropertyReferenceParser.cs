using Iql.JavaScript.Extensions;
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
            IqlExpression parent = null;
            if (action.Parent != null && (action.Parent.Type != IqlExpressionType.RootReference ||
                                          !string.IsNullOrWhiteSpace(parser.Adapter.RootVariableName)))
            {
                parent = action.Parent;
            }
            IqlExpression propertyName = new IqlFinalExpression(action.PropertyName);
            var accessorExpression = 
                parent == null 
                ? propertyName
                : parent.DotAccess(propertyName);
            if (parser.IsFilter)
            {
                if (parent != null)
                {
                    return parent.Coalesce(
                        accessorExpression);
                }
                return new IqlFinalExpression(action.PropertyName);
            }
            return accessorExpression;
        }
    }
}