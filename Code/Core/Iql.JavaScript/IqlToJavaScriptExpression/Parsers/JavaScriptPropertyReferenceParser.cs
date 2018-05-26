using Iql.JavaScript.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptPropertyReferenceParser : JavaScriptActionParserBase<IqlPropertyExpression>
    {
        public override IqlExpression ToQueryString(IqlPropertyExpression action,
            JavaScriptIqlParserInstance parser)
        {
            IqlExpression parent = null;
            if (action.Parent != null)
            {
                parent = action.Parent;
            }
            IqlExpression propertyName = new IqlFinalExpression<string>(action.PropertyName);
            //if (parser.IsFilter)
            //{

            //}
            if (parent != null)
            {
                if (parser.AllowTranspilation())
                {
                    return parent.Coalesce(
                        propertyName);
                }

                return parent.DotAccess(propertyName);
            }
            return new IqlFinalExpression<string>(action.PropertyName);
            //var accessorExpression =
            //    parent == null
            //        ? propertyName
            //        : parent.DotAccess(propertyName);
            //return accessorExpression;
        }
    }
}