using Iql.Data.DataStores.InMemory;
using Iql.Data.Queryable;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptWithKeyQueryParser : JavaScriptActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlWithKeyExpression action, JavaScriptIqlParserContext parser)
        {
            var iqlAnd = action.KeyEqualToExpressions.And();
            var javaScriptOutput = parser.Parse(iqlAnd);
            var js = javaScriptOutput.ToCodeString();
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.Where)}(function({javaScriptOutput.RootEntityParameterName}) {{ return {js}; }})"
            );
        }
    }
}