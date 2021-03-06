using Iql.Data.DataStores.InMemory;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptIntersectsActionParser : JavaScriptActionParserBase<IqlIntersectsExpression>
    {
        public override IqlExpression ToQueryString(IqlIntersectsExpression action, JavaScriptIqlParserContext parser)
        {
            var point = parser.Parse(action.Parent).ToCodeString();
            var polygon = parser.Parse(action.Polygon).ToCodeString();
            return new IqlFinalExpression<string>(
                $"(context||contextWrapper).{nameof(IInMemoryContext.Intersects)}({point}, {polygon})");
        }
    }
}