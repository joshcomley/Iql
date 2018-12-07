using Iql.Data.DataStores.InMemory;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptDistanceActionParser : JavaScriptActionParserBase<IqlDistanceExpression>
    {
        public override IqlExpression ToQueryString(IqlDistanceExpression action, JavaScriptIqlParserContext parser)
        {
            var left = parser.Parse(action.Left).ToCodeString();
            var right = parser.Parse(action.Right).ToCodeString();
            return new IqlFinalExpression<string>(
                $"context.{nameof(IInMemoryContext.DistanceBetween)}({left}, {right})");
        }
    }
}