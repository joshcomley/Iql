using System.Linq;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataEnumLiteralParser : ODataActionParserBase<IqlEnumLiteralExpression>
    {
        public override IqlExpression ToQueryString(IqlEnumLiteralExpression action,
            ODataIqlParserInstance parser)
        {
            return new IqlFinalExpression<string>($"'{action.Value.Sum(s => s.Value)}'");
        }
    }
}