using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.Parsing;
using Iql.Queryable.Expressions.Conversion;

namespace Iql.Queryable.IqlToIql
{
    public class IqlToIqlActionParserBase<TIqlExpression> :
        ActionParser<TIqlExpression,
            IqlToIqlIqlData,
            IqlToIqlExpressionAdapter,
            string,
            IqlToIqlIqlOutput,
            IqlToIqlParserInstance,
            IExpressionConverter>
        where TIqlExpression : IqlExpression
    { }
}