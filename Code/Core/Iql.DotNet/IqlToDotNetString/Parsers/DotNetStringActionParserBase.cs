using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringActionParserBase<TIqlExpression> :
        ActionParser<
            TIqlExpression,
            DotNetStringIqlData,
            DotNetStringIqlExpressionAdapter,
            string,
            DotNetStringOutput,
            DotNetStringIqlParserContext,
            DotNetExpressionConverter>
        where TIqlExpression : IqlExpression { }
}