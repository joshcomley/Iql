using System.Collections.Generic;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptCountActionParser : JavaScriptActionParserBase<IqlCountExpression>
    {
        public override IqlExpression ToQueryString(
            IqlCountExpression action,
            JavaScriptIqlParserContext parser)
        {
            var expressions = new List<IqlExpression>();
            expressions.Add(action.Parent);
            if (action.Value != null)
            {
                var lambda = parser.Parse(action.Value);
                if (lambda != null)
                {
                    var lambdaString = lambda.ToCodeString();
                    if (!string.IsNullOrWhiteSpace(lambdaString))
                    {
                        expressions.Add(new IqlFinalExpression<string>(
                            $".filter({lambdaString})"));
                    }
                }
            }
            expressions.Add(new IqlFinalExpression<string>(".length"));
            return new IqlParenthesisExpression(
                new IqlAggregateExpression(
                    expressions.ToArray()
                )
            );
        }
    }
}