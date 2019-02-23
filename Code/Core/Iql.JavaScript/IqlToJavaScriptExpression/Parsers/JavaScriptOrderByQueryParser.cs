using Iql.Data.DataStores.InMemory;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptOrderByQueryParser : JavaScriptActionParserBase<IqlOrderByExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlOrderByExpression action, JavaScriptIqlParserContext parser)
        {
            var js = parser.Parse(action.OrderExpression).ToCodeString();
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.OrderBy)}({js}, {(action.Descending ? "true" : "false")})"
            );
        }
    }
}