using Iql.Conversion;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlActionParserBase<TIqlExpression> :
        AsyncActionParser<TIqlExpression,
            IqlToIqlIqlData,
            IqlToIqlExpressionAdapter,
            string,
            IqlToIqlIqlOutput,
            IqlToIqlParserContext,
            IExpressionConverter>
        where TIqlExpression : IqlExpression
    { }
}