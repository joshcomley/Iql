using Iql.Data.DataStores.InMemory;
using Iql.JavaScript.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptExpandQueryParser : JavaScriptActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpandExpression action, JavaScriptIqlParserContext parser)
        {
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.Expand)}({action.SerializeDeserialize()})"
            );
        }
    }
}