using Iql.JavaScript.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptPropertyReferenceParser : JavaScriptActionParserBase<IqlPropertyExpression>
    {
        protected string Separator = ".";

        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            JavaScriptIqlParserInstance parser)
        {
            IqlExpression parent = null;
            if (action.Parent != null && (action.Parent.Type != IqlExpressionType.RootReference ||
                                          !string.IsNullOrWhiteSpace(parser.Adapter.RootVariableName)))
            {
                parent = action.Parent;
            }
            IqlExpression propertyName = new IqlFinalExpression<string>(action.PropertyName);
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
                return new IqlFinalExpression<string>(action.PropertyName);
            }
            return accessorExpression;
        }
    }
}