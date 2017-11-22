using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNet.Parsers
{
    public class DotNetActionParserBase<TIqlExpression> :
        ActionParser<
            TIqlExpression, 
            DotNetIqlData, 
            DotNetIqlExpressionAdapter,
            Expression,
            DotNetOutput, 
            DotNetIqlParserInstance>
        where TIqlExpression : IqlExpression
    {
    }
}