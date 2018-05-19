using Iql.Conversion;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
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