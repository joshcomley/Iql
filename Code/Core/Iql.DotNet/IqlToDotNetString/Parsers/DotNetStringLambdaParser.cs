using System.Linq;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringLambdaParser : DotNetStringActionParserBase<IqlLambdaExpression>
    {
        public override IqlExpression ToQueryString(
            IqlLambdaExpression action,
            DotNetStringIqlParserContext parser)
        {
            var parameters = "";
            if (action.Parameters != null)
            {
                parameters = string.Join(", ", action.Parameters.Select(p => parser.Parse(p
#if TypeScript
                , null
#endif
                    ).ToCodeString()));
                if (action.Parameters.Count > 1)
                {
                    parameters = $"({parameters})";
                }
            }
            var body = parser.Parse(action.Body
#if TypeScript
            , null
#endif
                ).ToCodeString();
            var expression = $"{parameters} => {body}";
            return new IqlFinalExpression<string>(expression);
        }
    }
}